# Підсумок виконаних робіт у цьому чаті

1. **UI правки вкладки користувача**
   - Прибрав чорну рамку активних табів у `UserProblemsTab`.
   - Перебудував layout: окрема зона для мапи, коментарів, опису та деталей проблеми.

2. **Редагування проблеми на фронтенді**
   - Додав можливість редагувати опис (`DescriptionBlock`) та назву/категорії (`ProblemDetailsCard`).
   - Реалізував кнопки перегляду зображень із disable-станами й тултипами.

3. **Створення сторінки всіх коментарів**
   - Імплементував `ProblemCommentsPage` та маршрут `/problems/:id/comments`.

4. **Додавання нових API методів і команд**
   - Створив окремі команди та DTO для оновлення опису, назви/категорій і локації проблеми.
   - Додав до контролера нові endpoints та мінімальний DTO `ProblemSummaryDto` для списків.

5. **Оновлення клієнтського API та типів**
   - Розширив `problemsApi` новими методами (updateDescription, updateTitleAndCategories, updateLocation) та мінімальним типом `ProblemSummary`.

6. **Можливість координатору змінити адресу проблеми**
   - На сторінці координатора додано режим "Змінити адресу" з картою та збереженням нових координат.

7. **Виправлення доменної моделі**
   - Повернув метод `UpdateProblem` у `Problem.cs`, що виправило помилку збірки.

## Перелік створених та змінених файлів

### API / Backend
- `api/src/Application/Problems/Commands/UpdateProblemDescriptionCommand.cs` *(новий)*
- `api/src/Application/Problems/Commands/UpdateProblemTitleAndCategoriesCommand.cs` *(новий)*
- `api/src/Application/Problems/Commands/UpdateProblemLocationCommand.cs` *(новий)*
- `api/src/API/DTOs/Problems/UpdateProblemDescriptionDto.cs` *(новий)*
- `api/src/API/DTOs/Problems/UpdateProblemTitleAndCategoriesDto.cs` *(новий)*
- `api/src/API/DTOs/Problems/UpdateProblemLocationDto.cs` *(новий)*
- `api/src/API/DTOs/Problems/ProblemSummaryDto.cs` *(новий)*
- `api/src/API/Controllers/ProblemsController.cs` *(оновлено)*
- `api/src/Domain/Problems/Problem.cs` *(оновлено)*

### Client / Frontend
- `client/src/features/problems/components/user-problems-tab.tsx` *(оновлено)*
- `client/src/features/problems/api/problems-api.ts` *(оновлено)*
- `client/src/pages/problem-comments-page.tsx` *(новий)*
- `client/src/routes/app-routes.tsx` *(оновлено)*
- `client/src/types/index.ts` *(оновлено)*
- `client/src/pages/coordinator-page.tsx` *(оновлено)*
