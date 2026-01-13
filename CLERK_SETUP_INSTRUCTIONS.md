# Інструкції з налаштування Clerk для OstrohProblems

## Огляд змін

Система автентифікації була мігрована з власної JWT-based системи на **Clerk** - сучасну платформу автентифікації з підтримкою:
- ✅ Email/Password автентифікація
- ✅ Google OAuth
- ✅ Facebook OAuth
- ✅ Управління користувачами
- ✅ Безпека та захист

---

## Крок 1: Створення акаунту Clerk

1. Перейдіть на [https://clerk.com](https://clerk.com)
2. Натисніть **"Sign Up"** та створіть безкоштовний акаунт
3. Підтвердіть email адресу

---

## Крок 2: Створення нового додатку (Application)

1. Після входу в Clerk Dashboard натисніть **"Create Application"**
2. Введіть назву додатку: `OstrohProblems` (або будь-яку іншу назву)
3. Оберіть методи автентифікації:
   - ✅ **Email** (обов'язково)
   - ✅ **Google** (для входу через Google)
   - ✅ **Facebook** (для входу через Facebook)
4. Натисніть **"Create Application"**

---

## Крок 3: Налаштування OAuth провайдерів

### Google OAuth

1. В Clerk Dashboard перейдіть до **"Configure" → "SSO Connections"**
2. Знайдіть **Google** та натисніть **"Configure"**
3. Вам потрібно створити OAuth credentials в Google Cloud Console:
   - Перейдіть на [Google Cloud Console](https://console.cloud.google.com)
   - Створіть новий проект або оберіть існуючий
   - Увімкніть **Google+ API**
   - Перейдіть до **"APIs & Services" → "Credentials"**
   - Натисніть **"Create Credentials" → "OAuth client ID"**
   - Оберіть тип: **Web application**
   - Додайте **Authorized redirect URIs** (скопіюйте з Clerk Dashboard)
   - Збережіть **Client ID** та **Client Secret**
4. Поверніться в Clerk Dashboard та введіть:
   - **Client ID** з Google
   - **Client Secret** з Google
5. Натисніть **"Save"**

### Facebook OAuth

1. В Clerk Dashboard перейдіть до **"Configure" → "SSO Connections"**
2. Знайдіть **Facebook** та натисніть **"Configure"**
3. Вам потрібно створити Facebook App:
   - Перейдіть на [Facebook Developers](https://developers.facebook.com)
   - Натисніть **"My Apps" → "Create App"**
   - Оберіть тип: **Consumer**
   - Введіть назву додатку та email
   - Додайте продукт **"Facebook Login"**
   - В налаштуваннях Facebook Login додайте **Valid OAuth Redirect URIs** (скопіюйте з Clerk Dashboard)
   - Збережіть **App ID** та **App Secret** з розділу Settings → Basic
4. Поверніться в Clerk Dashboard та введіть:
   - **App ID** з Facebook
   - **App Secret** з Facebook
5. Натисніть **"Save"**

---

## Крок 4: Налаштування ролей (Roles)

1. В Clerk Dashboard перейдіть до **"Configure" → "Roles"** (або "Sessions" → "Customize session token")
2. Додайте custom claims для ролей користувачів
3. В розділі **"Public Metadata"** додайте поле `role` з можливими значеннями:
   - `User` (звичайний користувач)
   - `Administrator` (адміністратор)
   - `Coordinator` (координатор)

---

## Крок 5: Налаштування Webhooks

Webhooks потрібні для синхронізації користувачів між Clerk та вашою базою даних.

1. В Clerk Dashboard перейдіть до **"Configure" → "Webhooks"**
2. Натисніть **"Add Endpoint"**
3. Введіть URL вашого API:
   ```
   https://your-api-domain.com/webhooks/clerk
   ```
   Для локальної розробки використовуйте [ngrok](https://ngrok.com) або [localtunnel](https://localtunnel.github.io/www/):
   ```
   https://your-ngrok-url.ngrok.io/webhooks/clerk
   ```
4. Оберіть події (Events):
   - ✅ `user.created`
   - ✅ `user.updated`
5. Натисніть **"Create"**
6. Збережіть **Signing Secret** (знадобиться для верифікації webhooks)

---

## Крок 6: Отримання API ключів

### Для Frontend (Client)

1. В Clerk Dashboard перейдіть до **"API Keys"**
2. Скопіюйте **Publishable Key** (починається з `pk_test_` або `pk_live_`)

### Для Backend (API)

1. В тому ж розділі **"API Keys"**
2. Знайдіть розділ **"JWT Templates"** або **"Sessions"**
3. Скопіюйте ваш **Clerk Domain** (наприклад: `your-app-name.clerk.accounts.dev`)

---

## Крок 7: Налаштування змінних середовища

### Backend (API)

1. Відкрийте файл `appsettings.json` в `api/src/API/`
2. Додайте або оновіть секцію Clerk:
   ```json
   {
     "Clerk": {
       "Domain": "your-app-name.clerk.accounts.dev"
     }
   }
   ```

### Frontend (Client)

1. Створіть файл `.env` в папці `client/`
2. Додайте наступні змінні:
   ```env
   VITE_API_URL=http://localhost:5146
   VITE_CLERK_PUBLISHABLE_KEY=pk_test_your_publishable_key_here
   ```

---

## Крок 8: Встановлення пакетів

### Backend (API)

Clerk SDK для .NET вже інтегрований через стандартні JWT Bearer токени. Додаткових пакетів не потрібно.

### Frontend (Client)

Встановіть Clerk React SDK:

```bash
cd client
npm install @clerk/clerk-react
```

---

## Крок 9: Запуск міграцій бази даних

Якщо потрібно створити нову міграцію для підтримки Clerk:

```bash
cd api/src/API
dotnet ef migrations add AddClerkSupport -p ../Infrastructure -s .
dotnet ef database update -p ../Infrastructure -s .
```

---

## Крок 10: Запуск додатку

### Backend

```bash
cd api/src/API
dotnet run
```

### Frontend

```bash
cd client
npm run dev
```

---

## Тестування

1. Відкрийте браузер та перейдіть на `http://localhost:5173/login`
2. Спробуйте:
   - Реєстрацію через email
   - Вхід через Google
   - Вхід через Facebook
3. Перевірте, що користувач створюється в базі даних
4. Перевірте CRUD операції для адміністратора

---

## Структура файлів

### Backend (API)

```
api/src/
├── API/
│   ├── Controllers/
│   │   └── ClerkWebhookController.cs       # Обробка Clerk webhooks
│   └── Program.cs                           # Оновлено для Clerk auth
├── Application/
│   └── Identity/
│       └── Commands/
│           └── SyncClerkUserCommand.cs      # Синхронізація користувачів
└── Infrastructure/
    ├── ConfigureClerkAuth.cs                # Налаштування Clerk JWT
    └── Services/
        └── ClerkIdentityService.cs          # Сервіс ідентифікації через Clerk
```

### Frontend (Client)

```
client/src/
├── contexts/
│   └── clerk-auth-provider.tsx              # Clerk Auth Provider
├── lib/
│   └── clerk-axios-instance.ts              # Axios з Clerk токенами
└── pages/
    ├── clerk-login-page.tsx                 # Сторінка входу з Clerk
    └── clerk-register-page.tsx              # Сторінка реєстрації з Clerk
```

---

## Важливі примітки

### Безпека

- ❗ **Ніколи не комітьте** `.env` файли з реальними ключами в Git
- ❗ Використовуйте `.env.example` як шаблон
- ❗ Для production використовуйте `pk_live_` та `sk_live_` ключі

### Ролі користувачів

За замовчуванням всі нові користувачі отримують роль `User`. Для зміни ролі:

1. В Clerk Dashboard перейдіть до **"Users"**
2. Оберіть користувача
3. В розділі **"Metadata" → "Public"** додайте:
   ```json
   {
     "role": "Administrator"
   }
   ```

### Локальна розробка з Webhooks

Для тестування webhooks локально:

1. Встановіть ngrok: `npm install -g ngrok`
2. Запустіть API: `dotnet run`
3. Запустіть ngrok: `ngrok http 5146`
4. Використайте ngrok URL в Clerk webhook settings

---

## Підтримка

Якщо виникли проблеми:

1. Перевірте Clerk Dashboard → Logs для помилок
2. Перевірте консоль браузера для frontend помилок
3. Перевірте логи API для backend помилок
4. Документація Clerk: [https://clerk.com/docs](https://clerk.com/docs)

---

## Що було збережено з попередньої системи

- ✅ Всі CRUD операції для користувачів (адмін панель)
- ✅ Управління ролями
- ✅ Профілі користувачів
- ✅ Зміна паролю (тепер через Clerk)
- ✅ Всі існуючі стилі форм входу та реєстрації

## Що змінилося

- ❌ Видалено власну JWT генерацію
- ❌ Видалено refresh tokens (Clerk керує цим автоматично)
- ❌ Видалено хешування паролів (Clerk керує цим)
- ✅ Додано OAuth через Google та Facebook
- ✅ Додано кращу безпеку через Clerk
- ✅ Додано автоматичну синхронізацію користувачів
