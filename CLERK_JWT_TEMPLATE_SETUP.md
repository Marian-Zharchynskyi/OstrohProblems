# Налаштування JWT Template в Clerk для передачі ролей

## ВАЖЛИВО: Цей крок обов'язковий для роботи автентифікації!

Clerk за замовчуванням не включає `public_metadata` в JWT токен. Потрібно налаштувати JWT Template.

---

## Крок 1: Відкрийте Clerk Dashboard

1. Перейдіть на [https://dashboard.clerk.com](https://dashboard.clerk.com)
2. Оберіть ваш проект **OstrohProblems**

---

## Крок 2: Налаштування JWT Template

1. В лівому меню перейдіть до **"JWT Templates"**
2. Натисніть **"New template"**
3. Оберіть **"Blank"**
4. В поле **"Name"** введіть: `ostroh-problems-token` (це ВАЖЛИВО, код очікує саме цю назву)
5. В розділі **"Claims"** додайте наступний JSON:

```json
{
  "public_metadata": "{{user.public_metadata}}",
  "email": "{{user.primary_email_address}}",
  "name": "{{user.full_name}}"
}
```

6. Натисніть **"Save"**

---

## Крок 3: Додайте роль для існуючих користувачів

### Для адміністратора (admin@ostroh.edu.ua):

1. Перейдіть до **"Users"** в Clerk Dashboard
2. Знайдіть користувача з email `admin@ostroh.edu.ua`
3. Натисніть на користувача
4. Перейдіть до вкладки **"Metadata"**
5. В розділі **"Public metadata"** додайте:
   ```json
   {
     "role": "Administrator"
   }
   ```
6. Натисніть **"Save"**

### Для координатора (coordinator@ostroh.edu.ua):

1. Знайдіть користувача з email `coordinator@ostroh.edu.ua`
2. В **"Public metadata"** додайте:
   ```json
   {
     "role": "Coordinator"
   }
   ```
3. Натисніть **"Save"**

### Для звичайного користувача (user@ostroh.edu.ua):

1. Знайдіть користувача з email `user@ostroh.edu.ua`
2. В **"Public metadata"** додайте:
   ```json
   {
     "role": "User"
   }
   ```
3. Натисніть **"Save"**

---

## Крок 4: Перевірка

1. Вийдіть з системи (якщо ви залогінені)
2. Увійдіть знову
3. Тепер система повинна правильно визначати вашу роль

---

## Альтернативний метод (якщо JWT Template не працює)

Якщо JWT Template не працює, можна використати Clerk API для отримання metadata:

1. В `appsettings.json` додайте:
   ```json
   {
     "Clerk": {
       "Domain": "your-app-name.clerk.accounts.dev",
       "SecretKey": "sk_test_your_secret_key_here"
     }
   }
   ```

2. Встановіть Clerk .NET SDK:
   ```bash
   cd api/src/Infrastructure
   dotnet add package Clerk.Net
   ```

Але краще використовувати JWT Template, оскільки це простіше і не вимагає додаткових API викликів.

---

## Можливі ролі в системі

- `Administrator` - повний доступ до системи
- `Coordinator` - доступ до координаторської панелі
- `User` - базовий доступ користувача

---

## Що робити якщо роль не визначається

1. Перевірте, що JWT Template збережений
2. Перевірте, що public_metadata додана для користувача
3. Вийдіть з системи та увійдіть знову (токен оновиться)
4. Перевірте консоль браузера на помилки
5. Перевірте логи API на помилки

---

## Налаштування для нових користувачів

Для автоматичного призначення ролі новим користувачам:

1. В Clerk Dashboard перейдіть до **"User & Authentication" → "Email, Phone, Username"**
2. Увімкніть **"Require email verification"**
3. Створіть Clerk Action (якщо доступно) або використовуйте webhook для автоматичного призначення ролі `User`

Або можна налаштувати в коді - middleware `ClerkUserSyncMiddleware` вже робить це автоматично при створенні користувача (JIT provisioning).
