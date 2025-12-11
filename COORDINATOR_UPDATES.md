# Оновлення для ролі Координатора та автентифікації

Дата: 2025-12-09

## Backend

- **Відхилення проблеми очищає координатора**
  - `Problem.cs`
    - Додано метод `ClearCoordinator()`:
      - Скидає `CoordinatorId` та `Coordinator` у `null`.
      - Оновлює `UpdatedAt`.
  - `RejectProblemCommand.cs`
    - Після `problem.Reject(...)` та `problem.UpdateStatus(ProblemStatus.Rejected)` додається виклик `problem.ClearCoordinator()`, щоб відхилена проблема знову стала "непризначеною".

- **Повернення проблеми з відхилених (Restore)**
  - `Problem.cs`
    - Додано метод `ClearRejection()` для очищення `RejectionReason` та оновлення `UpdatedAt`.
  - `RestoreProblemCommand.cs` (новий файл)
    - Нова команда `RestoreProblemCommand`:
      - Пробує знайти проблему за `ProblemId`.
      - Якщо знайдено, виконує:
        - `problem.UpdateStatus(ProblemStatus.New)`
        - `problem.ClearRejection()`
      - Оновлює проблему в репозиторії.
  - `ProblemsController.cs`
    - Додано ендпоінт:
      - `PUT /problems/restore/{problemId}`
      - Доступ: `Admin`, `Coordinator`.
      - Викликає `RestoreProblemCommand` і повертає оновлений `ProblemDto`.

## Frontend – проблеми та координатор

- **API-клієнт** – `client/src/features/problems/api/problems-api.ts`
  - Додано метод `restoreProblem(problemId: string)`:
    - `PUT /problems/restore/{problemId}`
    - Повертає оновлену проблему.

- **Сторінка координатора** – `client/src/pages/coordinator-page.tsx`

  ### Нові стани та фільтри
  - Додано локальний стейт:
    - `selectedStatus` – обраний у формі новий статус.
    - `activeTab: 'new' | 'my' | 'rejected'` – тепер три вкладки.
    - `actionMode` розширено значенням `'updateStatus'`.
  - Фільтри:
    - `newProblems` – як раніше: статус `New` та без координатора.
    - `rejectedProblems` – `p.status === ProblemStatusConstants.Rejected`.

  ### Вкладка "Відхилені"
  - Додано третю вкладку вгорі:
    - Кнопка "Відхилені ({rejectedProblems.length})".
  - Контент вкладки:
    - Список відхилених проблем з:
      - Назвою, статусом (червоний бейдж), описом.
      - За наявності – показується `rejectionReason`.

  ### Детальний перегляд проблеми
  - Для проблеми, де користувач є координатором і статус не `Completed` та не `Rejected`:
    - Залишено кнопки:
      - "Почати виконання" (для `New`).
      - "Оновити поточний стан".
      - "Завершити".
      - "Відхилити".
    - Додано кнопку "Змінити статус", яка відкриває нову форму.

  ### Комбінована форма "Статус + опис"
  - Замість інлайн `<select>` тепер:
    - Кнопка "Змінити статус" відкриває блок із формою:
      - Дропдаун "Новий статус" (`statusOptions`).
      - `Textarea` для опису змін / поточного стану (опційно).
      - Кнопка "Зберегти" викликає `handleStatusChange(detailProblem.id!)`.
      - Кнопка "Скасувати" скидає `selectedStatus` та `currentStateInput`.
  - `handleStatusChange(problemId: string)`:
    - Перевіряє, що `selectedStatus` заповнено.
    - Викликає `problemsApi.updateStatus(problemId, selectedStatus)`.
    - Якщо `currentStateInput` не порожній – додатково викликає `problemsApi.updateCurrentState(problemId, currentStateInput)`.
    - Очищає форму, закриває `actionMode`, інвалідовує кеш проблем.

  ### Повернення відхиленої проблеми (Restore)
  - Додано функцію `handleRestoreProblem(problemId: string)`:
    - Викликає `problemsApi.restoreProblem(problemId)`.
    - Показує toast про успіх, закриває detail view, інвалідовує кеш.
  - У детальному перегляді, якщо `detailProblem.status === ProblemStatusConstants.Rejected`:
    - Показується блок із поясненням, що проблему можна повернути до статусу "Нова".
    - Кнопка "Повернути проблему" викликає `handleRestoreProblem`.

## Frontend – автентифікація та зберігання токенів

- **`client/src/lib/token-storage.ts`**
  - Раніше токени зберігались у `localStorage`.
  - Замінено на `sessionStorage` для всіх операцій:
    - `getTokens()` читає з `sessionStorage`.
    - `setTokens()` записує в `sessionStorage`.
    - `clearTokens()` видаляє з `sessionStorage`.
    - `getAccessToken()` бере токен з `sessionStorage`.
  - Наслідок:
    - Кожна вкладка браузера має власний набір токенів.
    - Можна одночасно бути залогіненим різними користувачами/ролями в різних вкладках.
    - Після перезапуску браузера сесія не зберігається (на відміну від `localStorage`).

---

Цей файл підсумовує всі ключові зміни, пов’язані з роботою координатора, обробкою відхилених проблем та ізоляцією сесій користувачів по вкладках браузера.
