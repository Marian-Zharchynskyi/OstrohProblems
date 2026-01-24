# Виправлення бага автентифікації Clerk

## Проблема

При реєстрації або вході користувача перенаправляло на `http://localhost:3000/login/factor-one`, і система не розуміла хто користувач.

---

## Знайдені проблеми

### 1. Відсутні redirect URLs в Clerk компонентах
- `SignIn` та `SignUp` компоненти не мали налаштованих `afterSignInUrl` та `afterSignUpUrl`
- Clerk за замовчуванням перенаправляв на `/login/factor-one`

### 2. Public metadata не передавалась в JWT токені
- Clerk за замовчуванням не включає `public_metadata` в JWT токен
- Роль користувача зберігалась в `public_metadata.role`, але не була доступна на backend

### 3. Backend не міг отримати роль користувача
- `ClerkIdentityService` намагався прочитати `public_metadata` з claims, але її там не було
- Middleware `ClerkUserSyncMiddleware` не міг визначити роль при створенні користувача

---

## Виправлення

### 1. Frontend - додано redirect URLs

**Файл:** `client/src/pages/clerk-login-page.tsx`
```tsx
<SignIn 
  routing="path"
  path="/login"
  signUpUrl="/register"
  afterSignInUrl="/map"        // ✅ Додано
  forceRedirectUrl="/map"      // ✅ Додано
  appearance={{...}}
/>
```

**Файл:** `client/src/pages/clerk-register-page.tsx`
```tsx
<SignUp 
  routing="path"
  path="/register"
  signInUrl="/login"
  afterSignUpUrl="/map"        // ✅ Додано
  forceRedirectUrl="/map"      // ✅ Додано
  appearance={{...}}
/>
```

### 2. Backend - додано обробку public_metadata в JWT

**Файл:** `api/src/Infrastructure/ConfigureClerkAuth.cs`

Додано `OnTokenValidated` event handler, який:
- Читає `public_metadata` claim з JWT токену
- Парсить JSON і витягує роль
- Додає роль як `ClaimTypes.Role` claim

```csharp
OnTokenValidated = context =>
{
    if (context.Principal?.Identity is ClaimsIdentity identity)
    {
        var publicMetadataClaim = context.Principal.FindFirst("public_metadata")?.Value;
        if (!string.IsNullOrEmpty(publicMetadataClaim))
        {
            try
            {
                var metadata = JsonSerializer.Deserialize<Dictionary<string, object>>(publicMetadataClaim);
                if (metadata != null && metadata.ContainsKey("role"))
                {
                    var role = metadata["role"]?.ToString();
                    if (!string.IsNullOrEmpty(role))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }
            }
            catch { }
        }
    }
    return Task.CompletedTask;
}
```

### 3. Frontend - покращено отримання ролі

**Файл:** `client/src/contexts/clerk-auth-provider.tsx`

Спрощено логіку отримання ролі з Clerk:
```tsx
const role = (clerkUser.publicMetadata?.role as string) || 'User'

const userData: User = {
  id: response.data.id,
  email: clerkUser.primaryEmailAddress?.emailAddress || '',
  name: clerkUser.firstName || undefined,
  roles: [role]
}
```

---

## Необхідні дії в Clerk Dashboard

### ⚠️ КРИТИЧНО ВАЖЛИВО!

Потрібно налаштувати JWT Template в Clerk Dashboard, щоб `public_metadata`, `email` та `name` передавались в токені.
**Без полів email та name нові користувачі не будуть створюватись в базі даних (помилка 401)!**

**Детальні інструкції:** `CLERK_JWT_TEMPLATE_SETUP.md`

### Коротка інструкція:

1. Відкрийте [Clerk Dashboard](https://dashboard.clerk.com)
2. Перейдіть до **"JWT Templates"**
3. Оберіть або створіть template з назвою **`ostroh-problems-token`**
4. В розділі **"Claims"** додайте:
   ```json
   {
     "public_metadata": "{{user.public_metadata}}",
     "email": "{{user.primary_email_address}}",
     "name": "{{user.full_name}}"
   }
   ```
5. Збережіть

### Додайте ролі для існуючих користувачів:

**Адміністратор (admin@ostroh.edu.ua):**
```json
{
  "role": "Administrator"
}
```

**Координатор (coordinator@ostroh.edu.ua):**
```json
{
  "role": "Coordinator"
}
```

**Користувач (user@ostroh.edu.ua):**
```json
{
  "role": "User"
}
```

---

## Як перевірити що все працює

1. Налаштуйте JWT Template в Clerk Dashboard (див. вище)
2. Додайте роль в public_metadata для вашого користувача
3. Запустіть backend:
   ```bash
   cd api/src/API
   dotnet run
   ```
4. Запустіть frontend:
   ```bash
   cd client
   npm run dev
   ```
5. Відкрийте `http://localhost:5173/login`
6. Увійдіть або зареєструйтесь
7. Після входу вас має перенаправити на `/map`
8. Перевірте консоль браузера - не повинно бути помилок
9. Перевірте що ваша роль визначається правильно

---

## Змінені файли

### Frontend:
- ✅ `client/src/pages/clerk-login-page.tsx`
- ✅ `client/src/pages/clerk-register-page.tsx`
- ✅ `client/src/contexts/clerk-auth-provider.tsx`

### Backend:
- ✅ `api/src/Infrastructure/ConfigureClerkAuth.cs`

### Документація:
- ✅ `CLERK_JWT_TEMPLATE_SETUP.md` (новий файл)
- ✅ `CLERK_BUG_FIX_SUMMARY.md` (цей файл)

---

## Що робити якщо все ще не працює

### 1. Перевірте JWT Template
- Переконайтесь що `public_metadata` додана в Claims
- Збережіть зміни

### 2. Перевірте public_metadata користувача
- В Clerk Dashboard → Users → [ваш користувач] → Metadata
- Переконайтесь що `role` встановлена

### 3. Вийдіть та увійдіть знову
- Токен оновиться тільки після нового входу

### 4. Перевірте консоль браузера
- Відкрийте DevTools (F12)
- Перевірте вкладку Console на помилки
- Перевірте вкладку Network - чи успішні запити до API

### 5. Перевірте логи API
- Подивіться на вивід в терміналі де запущено `dotnet run`
- Шукайте помилки автентифікації

### 6. Перевірте Clerk Domain
- В `api/src/API/appsettings.json` має бути правильний Clerk Domain
- Формат: `your-app-name.clerk.accounts.dev`

---

## Додаткова інформація

### Як працює автентифікація тепер:

1. Користувач входить через Clerk (email/password або OAuth)
2. Clerk генерує JWT токен з `public_metadata`
3. Frontend отримує токен і зберігає його
4. При кожному запиті до API токен передається в заголовку `Authorization`
5. Backend перевіряє токен і витягує `public_metadata.role`
6. Роль додається як `ClaimTypes.Role` claim
7. Middleware `ClerkUserSyncMiddleware` створює/оновлює користувача в БД
8. `ClerkIdentityService` використовує claims для визначення ролі
9. Контролери перевіряють роль через `[Authorize(Roles = "...")]`

### Нові користувачі:

При реєстрації нового користувача:
- Clerk створює користувача
- Middleware `ClerkUserSyncMiddleware` автоматично створює запис в БД
- За замовчуванням призначається роль `User`
- Адміністратор може змінити роль через Clerk Dashboard або API

---

## Контакти для підтримки

Якщо виникли питання:
- Документація Clerk: https://clerk.com/docs
- Документація проекту: `CLERK_SETUP_INSTRUCTIONS.md`
