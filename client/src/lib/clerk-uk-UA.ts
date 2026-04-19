export const ukUA = {
  // Social buttons
  socialButtonsBlockButton: 'Продовжити через {{provider|titleize}}',
  socialButtonsBlockButtonAriaLabel: 'Продовжити через {{provider|titleize}}',

  // Sign In
  signIn: {
    start: {
      title: 'Вхід',
      subtitle: 'Увійдіть до вашого акаунту',
      actionText: 'Не маєте акаунту ?',
      actionLink: 'Зареєструватися',
      forgotPasswordLink: "Не пам'ятаю пароль",
      badge__lastUsed: 'Останній використаний',
      badge__preferred: 'Рекомендований',
      lastUsed: 'Останній використаний',
    },
    forgotPasswordLink: "Не пам'ятаю пароль",
    badge__lastUsed: 'Останній використаний',
    badge__preferred: 'Рекомендований',
    lastUsed: 'Останній використаний',
    factorOne: {
      forgotPasswordLink: "Не пам'ятаю пароль",
    },
    factorTwo: {
      forgotPasswordLink: "Не пам'ятаю пароль",
    },
    emailLink: {
      title: 'Перевірте вашу пошту',
      subtitle: 'Ми надіслали посилання для входу на {{emailAddress}}',
      formTitle: 'Посилання для входу',
      formSubtitle: 'Використайте посилання, надіслане на вашу пошту',
      resendButton: 'Не отримали посилання? Надіслати знову',
      verified: {
        title: 'Вхід виконано успішно',
      },
      loading: {
        title: 'Виконується вхід...',
      },
      verifiedSwitchTab: {
        title: 'Посилання підтверджено',
        subtitle: 'Поверніться на новостворену вкладку, щоб продовжити',
        subtitleNewTab: 'Поверніться на попередню вкладку, щоб продовжити',
      },
    },
    password: {
      title: 'Введіть пароль',
      subtitle: 'для продовження до {{applicationName}}',
      actionLink: 'Використати інший метод',
      forgotPasswordLink: "Не пам'ятаю пароль",
    },
    phoneCode: {
      title: 'Перевірте ваш телефон',
      subtitle: 'Введіть код, надісланий на {{phoneNumber}}',
      formTitle: 'Код підтвердження',
      formSubtitle: 'Введіть код підтвердження, надісланий на ваш телефон',
      resendButton: 'Не отримали код? Надіслати знову',
    },
    emailCode: {
      title: 'Перевірте вашу пошту',
      subtitle: 'Введіть код, надісланий на {{emailAddress}}',
      formTitle: 'Код підтвердження',
      formSubtitle: 'Введіть код підтвердження, надісланий на вашу пошту',
      resendButton: 'Не отримали код? Надіслати знову',
    },
    alternativeMethods: {
      title: 'Використати інший метод',
      subtitle: 'Виникла проблема? Ви можете увійти іншим способом.',
      actionLink: 'Отримати допомогу',
      blockButton__emailCode: 'Надіслати код на {{identifier}}',
      blockButton__emailLink: 'Надіслати посилання на {{identifier}}',
      blockButton__password: 'Увійти з паролем',
      blockButton__phoneCode: 'Надіслати SMS код на {{identifier}}',
      blockButton__totp: 'Використати код автентифікатора',
      blockButton__backupCode: 'Використати резервний код',
      getHelp: {
        title: 'Отримати допомогу',
        content: 'Якщо у вас виникли труднощі із входом до вашого акаунту, надішліть нам електронного листа, і ми попрацюємо з вами, щоб якомога швидше відновити доступ.',
        blockButton__emailSupport: 'Написати листа на підтримку',
      },
      forgotPasswordLink: "Не пам'ятаю пароль",
      blockButton__forgotPassword: "Не пам'ятаю пароль",
    },
    forgotPassword: {
      title: 'Скинути пароль',
      subtitle: 'Введіть вашу електронну пошту',
      subtitle_email: 'Спочатку введіть код підтвердження, надісланий на вашу електронну пошту',
      subtitle_phone: 'Спочатку введіть код підтвердження, надісланий на ваш телефон',
      formTitle: 'Код скидання пароля',
      formSubtitle: 'Введіть код, надісланий на вашу пошту',
      resendButton: 'Не отримали код? Надіслати знову',
    },
    forgotPasswordAlternativeMethods: {
      title: "Не пам'ятаєте пароль?",
      label__alternativeMethods: 'Або виберіть інший спосіб входу.',
      blockButton__resetPassword: 'Скинути ваш пароль',
    },
    resetPassword: {
      title: 'Скинути пароль',
      formButtonPrimary: 'Скинути пароль',
      successMessage: 'Ваш пароль успішно змінено. Виконується вхід, будь ласка, зачекайте.',
      requiredMessage: 'З міркувань безпеки потрібно скинути ваш пароль.',
    },
    resetPasswordMfa: {
      detailsLabel: 'Нам потрібно підтвердити вашу особу перед скиданням пароля.',
    },
    noAvailableMethods: {
      title: 'Не вдається увійти',
      subtitle: 'Сталася помилка',
      message: 'Не вдається продовжити вхід. Немає доступних методів автентифікації.',
    },
    passkey: {
      title: 'Використати ваш ключ доступу',
      subtitle: 'Використовуйте ваш ключ доступу для підтвердження особи.',
    },
    accountSwitcher: {
      title: 'Виберіть акаунт',
      subtitle: 'Виберіть акаунт для продовження',
      action__addAccount: 'Додати акаунт',
      action__signOutAll: 'Вийти з усіх акаунтів',
    },
    // Factor one and factor two specific
    factor1: {
      title: 'Підтвердження особи',
      subtitle: 'Для продовження підтвердіть вашу особу.',
    },
    factor2: {
      title: 'Двофакторне підтвердження',
      subtitle: 'Для продовження вам потрібно підтвердити вхід.',
    },
  },

  // Sign Up
  signUp: {
    start: {
      title: 'Реєстрація',
      subtitle: 'Створіть новий акаунт',
      actionText: 'Вже маєте акаунт ?',
      actionLink: 'Увійти',
    },
    emailLink: {
      title: 'Перевірте вашу пошту',
      subtitle: 'Ми надіслали посилання для верифікації на {{emailAddress}}',
      formTitle: 'Посилання для верифікації',
      formSubtitle: 'Використайте посилання, надіслане на вашу пошту',
      resendButton: 'Не отримали посилання? Надіслати знову',
      verified: {
        title: 'Пошту успішно підтверджено',
      },
      loading: {
        title: 'Підтвердження...',
      },
      verifiedSwitchTab: {
        title: 'Пошту успішно підтверджено',
        subtitle: 'Поверніться на новостворену вкладку, щоб продовжити',
        subtitleNewTab: 'Поверніться на попередню вкладку, щоб продовжити',
      },
    },
    emailCode: {
      title: 'Підтвердіть вашу пошту',
      subtitle: 'Введіть код, надісланий на {{emailAddress}}',
      formTitle: 'Код підтвердження',
      formSubtitle: 'Введіть код підтвердження, надісланий на вашу пошту',
      resendButton: 'Не отримали код? Надіслати знову',
    },
    phoneCode: {
      title: 'Підтвердіть ваш телефон',
      subtitle: 'Введіть код, надісланий на {{phoneNumber}}',
      formTitle: 'Код підтвердження',
      formSubtitle: 'Введіть код підтвердження, надісланий на ваш телефон',
      resendButton: 'Не отримали код? Надіслати знову',
    },
    continue: {
      title: 'Заповніть відсутні поля',
      subtitle: 'для продовження до {{applicationName}}',
      actionText: 'Вже маєте акаунт?',
      actionLink: 'Увійти',
    },
  },

  // User profile
  userProfile: {
    start: {
      headerTitle__account: 'Акаунт',
      headerTitle__security: 'Безпека',
      headerSubtitle__account: 'Керуйте інформацією вашого акаунту',
      headerSubtitle__security: 'Керуйте налаштуваннями безпеки',
      profileSection: {
        title: 'Профіль',
      },
      usernameSection: {
        title: "Ім'я користувача",
        primaryButton__changeUsername: "Змінити ім'я користувача",
        primaryButton__setUsername: "Встановити ім'я користувача",
      },
      emailAddressesSection: {
        title: 'Електронні адреси',
        primaryButton: 'Додати електронну адресу',
        detailsTitle__primary: 'Основна електронна адреса',
        detailsSubtitle__primary: 'Ця електронна адреса є основною електронною адресою',
        detailsAction__primary: 'Завершити верифікацію',
        detailsTitle__nonPrimary: 'Встановити як основну електронну адресу',
        detailsSubtitle__nonPrimary: 'Встановіть цю електронну адресу як основну для отримання повідомлень про ваш акаунт.',
        detailsAction__nonPrimary: 'Встановити як основну',
        detailsTitle__unverified: 'Не підтверджена електронна адреса',
        detailsSubtitle__unverified: 'Ця електронна адреса не підтверджена і може бути обмежена в функціональності',
        detailsAction__unverified: 'Завершити верифікацію',
        destructiveActionTitle: 'Видалити',
        destructiveActionSubtitle: 'Видалити цю електронну адресу та прибрати її з вашого акаунту',
        destructiveAction: 'Видалити електронну адресу',
      },
      phoneNumbersSection: {
        title: 'Номери телефонів',
        primaryButton: 'Додати номер телефону',
        detailsTitle__primary: 'Основний номер телефону',
        detailsSubtitle__primary: 'Цей номер телефону є основним номером телефону',
        detailsAction__primary: 'Завершити верифікацію',
        detailsTitle__nonPrimary: 'Встановити як основний номер телефону',
        detailsSubtitle__nonPrimary: 'Встановіть цей номер телефону як основний для отримання повідомлень про ваш акаунт.',
        detailsAction__nonPrimary: 'Встановити як основний',
        detailsTitle__unverified: 'Не підтверджений номер телефону',
        detailsSubtitle__unverified: 'Цей номер телефону не підтверджений і може бути обмежений в функціональності',
        detailsAction__unverified: 'Завершити верифікацію',
        destructiveActionTitle: 'Видалити',
        destructiveActionSubtitle: 'Видалити цей номер телефону та прибрати його з вашого акаунту',
        destructiveAction: 'Видалити номер телефону',
      },
      connectedAccountsSection: {
        title: 'Підключені акаунти',
        primaryButton: 'Підключити акаунт',
        subtitle__reauthorize: 'Потрібні дозволи було оновлено, і ви можете мати обмежену функціональність. Будь ласка, повторно авторизуйте цей застосунок, щоб уникнути проблем.',
        actionLabel__connectionFailed: 'Повторити спробу',
        actionLabel__reauthorize: 'Авторизувати зараз',
        destructiveActionTitle: 'Видалити',
        destructiveActionSubtitle: 'Видалити цей підключений акаунт з вашого акаунту',
        destructiveActionAccordionSubtitle: 'Видалити підключений акаунт',
      },
      passwordSection: {
        title: 'Пароль',
        primaryButton__changePassword: 'Змінити пароль',
        primaryButton__setPassword: 'Встановити пароль',
      },
      mfaSection: {
        title: 'Двофакторна автентифікація',
        primaryButton: 'Додати двофакторну автентифікацію',
        phoneCode: {
          destructiveActionTitle: 'Видалити',
          destructiveActionSubtitle: 'Видалити цей номер телефону з методів двофакторної автентифікації',
          destructiveActionLabel: 'Видалити номер телефону',
          title__default: 'Фактор за замовчуванням',
          title__setDefault: 'Встановити як фактор за замовчуванням',
          subtitle__default: 'Цей фактор буде використовуватися як метод двофакторної автентифікації за замовчуванням при вході.',
          subtitle__setDefault: 'Встановіть цей фактор як за замовчуванням для використання як метод двофакторної автентифікації за замовчуванням при вході.',
          actionLabel__setDefault: 'Встановити як за замовчуванням',
        },
        backupCodes: {
          headerTitle: 'Резервні коди',
          title__regenerate: 'Згенерувати нові резервні коди',
          subtitle__regenerate: 'Отримайте новий набір захищених резервних кодів. Попередні резервні коди будуть видалені і не можуть бути використані.',
          actionLabel__regenerate: 'Згенерувати коди',
        },
        totp: {
          headerTitle: 'Застосунок-автентифікатор',
          title: 'Фактор за замовчуванням',
          subtitle: 'Цей фактор буде використовуватися як метод двофакторної автентифікації за замовчуванням при вході.',
          destructiveActionTitle: 'Видалити',
          destructiveActionSubtitle: 'Видалити застосунок-автентифікатор з методів двофакторної автентифікації',
          destructiveActionLabel: 'Видалити застосунок-автентифікатор',
        },
      },
      activeDevicesSection: {
        title: 'Активні пристрої',
        primaryButton: 'Активні пристрої',
        detailsTitle: 'Поточний пристрій',
        detailsSubtitle: 'Це пристрій, який ви зараз використовуєте',
        destructiveActionTitle: 'Вийти',
        destructiveActionSubtitle: 'Вийти з вашого акаунту на цьому пристрої',
        destructiveAction: 'Вийти з пристрою',
      },
      web3WalletsSection: {
        title: 'Web3 гаманці',
        primaryButton: 'Web3 гаманці',
        destructiveActionTitle: 'Видалити',
        destructiveActionSubtitle: 'Видалити цей Web3 гаманець з вашого акаунту',
        destructiveAction: 'Видалити гаманець',
      },
      dangerSection: {
        title: 'Небезпечна зона',
        deleteAccountButton: 'Видалити акаунт',
        deleteAccountTitle: 'Видалити акаунт',
        deleteAccountDescription: "Видалити ваш акаунт та всі пов'язані з ним дані",
      },
    },
    profilePage: {
      title: 'Оновити профіль',
      imageFormTitle: 'Зображення профілю',
      imageFormSubtitle: 'Завантажити зображення',
      imageFormDestructiveActionSubtitle: 'Видалити зображення',
      fileDropAreaTitle: 'Перетягніть файл сюди або...',
      fileDropAreaAction: 'Вибрати файл',
      fileDropAreaHint: 'Завантажте зображення у форматі JPG, PNG, GIF або WEBP. Розмір файлу не повинен перевищувати 10 МБ.',
      readonly: 'Інформація вашого профілю надана підключенням підприємства і не може бути редагована.',
      successMessage: 'Ваш профіль було оновлено.',
    },
    mfaPage: {
      title: 'Додати двофакторну автентифікацію',
      formHint: 'Виберіть метод для додавання.',
    },
    mfaTOTPPage: {
      title: 'Додати застосунок-автентифікатор',
      verifyTitle: 'Код підтвердження',
      verifySubtitle: 'Введіть код підтвердження, згенерований вашим автентифікатором',
      successMessage: 'Двофакторну автентифікацію тепер увімкнено. При вході вам потрібно буде ввести код підтвердження з цього автентифікатора як додатковий крок.',
      authenticatorApp: {
        infoText__ableToScan: "Налаштуйте новий метод входу у вашому застосунку-автентифікаторі та відскануйте наступний QR-код, щоб зв'язати його з вашим акаунтом.",
        infoText__unableToScan: 'Налаштуйте новий метод входу у вашому автентифікаторі та введіть ключ, наданий нижче.',
        inputLabel__unableToScan1: "Переконайтеся, що увімкнено одноразові паролі на основі часу, потім завершіть зв'язування вашого акаунту.",
        inputLabel__unableToScan2: 'Альтернативно, якщо ваш автентифікатор підтримує TOTP URI, ви також можете скопіювати повний URI.',
        buttonAbleToScan__nonPrimary: 'Замість цього відсканувати QR-код',
        buttonUnableToScan__nonPrimary: 'Не вдається відсканувати QR-код?',
      },
    },
    mfaPhoneCodePage: {
      title: 'Додати підтвердження через SMS-код',
      primaryButton__addPhoneNumber: 'Додати номер телефону',
      subtitle__availablePhoneNumbers: 'Виберіть номер телефону для реєстрації на підтвердження через SMS-код.',
      subtitle__unavailablePhoneNumbers: 'Немає доступних номерів телефону для реєстрації на підтвердження через SMS-код.',
      successMessage:
        'Підтвердження через SMS-код тепер увімкнено для цього номера телефону. При вході вам потрібно буде ввести код підтвердження, надісланий на цей номер телефону, як додатковий крок.',
      removeResource: {
        title: 'Видалити підтвердження через SMS-код',
        messageLine1: '{{identifier}} більше не буде отримувати коди підтвердження при вході.',
        messageLine2: 'Ваш акаунт може бути менш захищеним. Ви впевнені, що хочете продовжити?',
        successMessage: 'Підтвердження через SMS-код було видалено для {{mfaPhoneCode}}',
      },
    },
    backupCodePage: {
      title: 'Додати підтвердження через резервний код',
      title__codelist: 'Резервні коди',
      subtitle__codelist: 'Зберігайте їх у безпечному місці та тримайте в таємниці.',
      infoText1: 'Резервні коди будуть увімкнені для цього акаунту.',
      infoText2: 'Тримайте резервні коди в таємниці та зберігайте їх у безпечному місці. Ви можете згенерувати нові резервні коди, якщо підозрюєте, що вони були скомпрометовані.',
      successSubtitle: 'Ви можете використовувати один з них для входу у ваш акаунт, якщо втратите доступ до вашого пристрою автентифікації.',
      successMessage:
        'Резервні коди тепер увімкнені. Ви можете використовувати один з них для входу у ваш акаунт, якщо втратите доступ до вашого пристрою автентифікації. Кожен код можна використовувати лише один раз.',
      actionLabel__copy: 'Копіювати все',
      actionLabel__copied: 'Скопійовано!',
      actionLabel__download: 'Завантажити .txt',
      actionLabel__print: 'Друкувати',
    },
    deletePage: {
      title: 'Видалити акаунт',
      messageLine1: 'Ви впевнені, що хочете видалити ваш акаунт?',
      messageLine2: 'Ця дія є остаточною та незворотною.',
      actionDescription: 'Введіть "Видалити акаунт" нижче для підтвердження.',
      confirm: 'Видалити акаунт',
    },
  },

  // User button
  userButton: {
    action__manageAccount: 'Керувати акаунтом',
    action__signOut: 'Вийти',
    action__signOutAll: 'Вийти з усіх акаунтів',
    action__addAccount: 'Додати акаунт',
  },

  // Form fields
  formFieldLabel__emailAddress: 'Електронна пошта',
  formFieldAction__forgotPassword: "Не пам'ятаю пароль",
  formFieldLabel__emailAddress_username: "Електронна пошта або ім'я користувача",
  formFieldLabel__username: "Ім'я користувача",
  formFieldLabel__phoneNumber: 'Номер телефону',
  formFieldLabel__password: 'Пароль',
  formFieldLabel__newPassword: 'Новий пароль',
  formFieldLabel__confirmPassword: 'Підтвердіть пароль',
  formFieldLabel__currentPassword: 'Поточний пароль',
  formFieldLabel__signOutOfOtherSessions: 'Вийти з інших пристроїв',
  formFieldLabel__firstName: "Ім'я",
  formFieldLabel__lastName: 'Прізвище',
  formFieldLabel__role: 'Роль',
  formFieldLabel__organizationName: 'Назва організації',
  formFieldLabel__organizationSlug: 'Ідентифікатор організації',
  formFieldLabel__organizationDomain: 'Домен',
  formFieldLabel__organizationDomainEmailAddress: 'Електронна пошта для верифікації',
  formFieldLabel__organizationDomainEmailAddressDescription: 'Введіть електронну адресу під цим доменом для верифікації та приєднання до організації.',
  formFieldLabel__confirmDeletion: 'Підтвердження',

  // Form field placeholders
  formFieldInputPlaceholder__emailAddress: 'name@example.com',
  formFieldInputPlaceholder__emailAddress_username: "name@example.com або ім'я користувача",
  formFieldInputPlaceholder__emailAddresses: 'name@example.com, name2@example.com',
  formFieldInputPlaceholder__phoneNumber: '+380',
  formFieldInputPlaceholder__username: "ім'я_користувача",
  formFieldInputPlaceholder__firstName: "Ваше ім'я",
  formFieldInputPlaceholder__lastName: 'Ваше прізвище',
  formFieldInputPlaceholder__backupCode: '',
  formFieldInputPlaceholder__organizationName: 'Моя організація',
  formFieldInputPlaceholder__organizationSlug: 'my-org',
  formFieldInputPlaceholder__organizationDomain: 'example.com',
  formFieldInputPlaceholder__organizationDomainEmailAddress: 'name@example.com',
  formFieldInputPlaceholder__confirmDeletionUserAccount: 'Видалити акаунт',
  formFieldInputPlaceholder__password: 'Введіть ваш пароль',

  // Form field errors
  formFieldError__notMatchingPasswords: 'Паролі не співпадають.',
  formFieldError__matchingPasswords: 'Паролі співпадають.',
  formFieldError__verificationLinkExpired: 'Термін дії посилання закінчився. Будь ласка, запросіть нове посилання.',

  // Form field hints
  formFieldHintText__optional: "Необов'язково",
  formFieldHintText__slug: 'Ідентифікатор - це читабельний ID, який має бути унікальним. Часто використовується в URL.',

  // Form buttons
  formButtonPrimary: 'Продовжити',
  formButtonPrimary__verify: 'Підтвердити',

  // Footer
  footerActionLink__useAnotherMethod: 'Використати інший метод',
  footerActionLink__help: 'Потрібна допомога?',
  footerActionLink__privacy: 'Конфіденційність',
  footerActionLink__terms: 'Умови',

  // Miscellaneous
  paginationButton__previous: 'Попередня',
  paginationButton__next: 'Наступна',
  paginationRowText__displaying: 'Відображається',
  paginationRowText__of: 'з',

  // Divider
  dividerText: 'або',

  // Verification
  identityPreviewEditButton: 'Змінити',
  identityPreviewEditButtonLabel: 'Змінити {{identifier}}',

  // Badges
  badge__default: 'За замовчуванням',
  badge__otherImpersonatorDevice: 'Інший пристрій імітатора',
  badge__primary: 'Основний',
  badge__requiresAction: 'Потрібна дія',
  badge__unverified: 'Не підтверджено',
  badge__userDevice: 'Пристрій користувача',
  badge__you: 'Ви',
  badge__lastUsed: 'Останній використаний',
  badge__preferred: 'Рекомендований',
  badge__last_used: 'Останній використаний',
  badge__lastActive: 'Остання активність',
  badge__last_active_at: 'Остання активність',
  badge__last_sign_in_at: 'Останній вхід',
  badge__active: 'Активний',
  lastUsed: 'Останній використаний',
  badge: {
    lastUsed: 'Останній використаний',
    last_used: 'Останній використаний',
    preferred: 'Рекомендований',
    primary: 'Основний',
  },

  // Buttons
  backButton: 'Назад',
  continue: 'Продовжити',

  // Dates
  dates: {
    lastDay: "Вчора о {{ date | timeString('uk-UA') }}",
    next6Days: "{{ date | weekday('uk-UA','long') }} о {{ date | timeString('uk-UA') }}",
    nextDay: "Завтра о {{ date | timeString('uk-UA') }}",
    numeric: "{{ date | numeric('uk-UA') }}",
    previous6Days: "Минулої {{ date | weekday('uk-UA','long') }} о {{ date | timeString('uk-UA') }}",
    sameDay: "Сьогодні о {{ date | timeString('uk-UA') }}",
  },

  // Sign in factor texts
  signInNewDevice: 'Ви входите з нового пристрою. Ми запитуємо підтвердження для забезпечення безпеки вашого акаунту.',

  // Alternative methods
  __experimental_userVerification: {
    alternativeMethods: {
      actionLink: 'Отримати допомогу',
      blockButton__password: 'Увійти з паролем',
    },
  },

  // Errors
  unstable__errors: {
    form_identifier_not_found: 'Не вдалося знайти ваш акаунт.',
    form_password_incorrect: 'Неправильний пароль. Будь ласка, спробуйте ще раз.',
    form_password_pwned: 'Цей пароль був скомпрометований в результаті витоку даних і не може бути використаний. Будь ласка, спробуйте інший пароль.',
    form_username_invalid_length: "Ім'я користувача має неправильну довжину.",
    form_username_invalid_character: "Ім'я користувача містить недопустимий символ.",
    form_param_format_invalid: 'Формат параметра недійсний.',
    form_param_format_invalid__email_address: 'Електронна адреса має бути дійсною адресою електронної пошти.',
    form_param_format_invalid__phone_number: 'Номер телефону має бути у дійсному міжнародному форматі',
    form_param_nil: 'Параметр не може бути порожнім.',
    form_password_length_too_short: 'Пароль занадто короткий.',
    form_param_max_length_exceeded__name: "Ім'я занадто довге.",
    form_password_not_strong_enough: 'Ваш пароль недостатньо надійний.',
    form_password_size_in_bytes_exceeded: 'Ваш пароль перевищив максимальну кількість байтів, будь ласка, скоротіть його або видаліть деякі спеціальні символи.',
    form_code_incorrect: 'Код невірний. Будь ласка, спробуйте ще раз.',
    not_allowed_access: 'Доступ заборонено.',
    form_identifier_exists: 'Цей ідентифікатор вже використовується.',
    passwordComplexity: {
      sentencePrefix: 'Ваш пароль повинен містити',
      minimumLength: 'мінімум {{length}} символів',
      maximumLength: 'менше ніж {{length}} символів',
      requireNumbers: 'цифру',
      requireLowercase: 'малу літеру',
      requireUppercase: 'велику літеру',
      requireSpecialCharacter: 'спеціальний символ',
    },
    zxcvbn: {
      notEnough: 'Ваш пароль недостатньо надійний.',
      couldBeStronger: 'Ваш пароль працює, але міг би бути надійнішим. Спробуйте додати більше символів.',
      goodPassword: 'Ваш пароль відповідає всім необхідним вимогам.',
      warnings: {
        straightRow: 'Прямі ряди клавіш на клавіатурі легко вгадати.',
        keyPattern: 'Короткі шаблони клавіатури легко вгадати.',
        simpleRepeat: 'Повторювані символи, такі як "aaa", легко вгадати.',
        extendedRepeat: 'Повторювані шаблони символів, такі як "abcabcabc", легко вгадати.',
        sequences: 'Поширені послідовності символів, такі як "abc", легко вгадати.',
        recentYears: 'Останні роки легко вгадати.',
        dates: 'Дати легко вгадати.',
        topTen: 'Це дуже поширений пароль.',
        topHundred: 'Це часто використовуваний пароль.',
        common: 'Це поширений пароль.',
        similarToCommon: 'Це схоже на поширений пароль.',
        wordByItself: 'Окремі слова легко вгадати.',
        namesByThemselves: 'Окремі імена або прізвища легко вгадати.',
        commonNames: 'Поширені імена та прізвища легко вгадати.',
        userInputs: "Не повинно бути жодних особистих або пов'язаних зі сторінкою даних.",
        pwned: 'Ваш пароль був розкритий в результаті витоку даних в Інтернеті.',
      },
      suggestions: {
        l33t: 'Уникайте передбачуваних замін літер, таких як "@" замість "a".',
        reverseWords: 'Уникайте зворотного написання поширених слів.',
        allUppercase: 'Використовуйте великі літери для деяких, але не всіх літер.',
        capitalization: 'Використовуйте великі літери не тільки для першої літери.',
        dates: "Уникайте дат та років, пов'язаних з вами.",
        recentYears: 'Уникайте останніх років.',
        associatedYears: "Уникайте років, пов'язаних з вами.",
        sequences: 'Уникайте поширених послідовностей символів.',
        repeated: 'Уникайте повторюваних слів та символів.',
        longerKeyboardPattern: 'Використовуйте довші шаблони клавіатури та змінюйте напрямок набору кілька разів.',
        anotherWord: 'Додайте більше менш поширених слів.',
        useWords: 'Використовуйте кілька слів, але уникайте поширених фраз.',
        noNeed: 'Ви можете створювати надійні паролі без використання символів, цифр або великих літер.',
        pwned: 'Якщо ви використовуєте цей пароль в іншому місці, вам слід змінити його.',
      },
    },
  },

  // Sign in methods
  signInWith: {
    password: 'Продовжити з паролем',
    email: 'Продовжити з електронною поштою',
    phone: 'Продовжити з номером телефону',
    passkey: 'Продовжити з ключем доступу',
  },

  // Device texts
  signIn_newDeviceVerification: {
    title: 'Підтвердьте вхід',
    subtitle: 'Ви входите з нового пристрою.',
    subtitle__message: 'Ми запитуємо підтвердження для забезпечення безпеки вашого акаунту.',
  },

  // MFA specific
  mfa__factorOneSubtitle: 'Для продовження виберіть метод підтвердження.',
  mfa__factorTwoSubtitle: 'Для продовження введіть код з вашого автентифікатора.',

  // Reset password specific
  resetPasswordMfa__detailsLabel: 'Нам потрібно підтвердити вашу особу перед скиданням пароля.',

  // Account switcher
  reverification: {
    alternativeMethods: {
      actionLink: 'Отримати допомогу',
    },
  },

  // Footer page links
  footerPageLink__help: 'Допомога',
  footerPageLink__privacy: 'Конфіденційність',
  footerPageLink__terms: 'Умови',

  // SSO methods
  signIn__sso_callback: {
    loading: 'Виконується вхід...',
  },

  // Factor one/two flows
  __experimental_alternativeMethods: {
    actionLink: 'Отримати допомогу',
    actionText: 'Не маєте доступу?',
    blockButton__backupCode: 'Використати резервний код',
    blockButton__emailCode: 'Надіслати код на {{identifier}}',
    blockButton__emailLink: 'Надіслати посилання на {{identifier}}',
    blockButton__passkey: 'Увійти з ключем доступу',
    blockButton__password: 'Увійти з паролем',
    blockButton__phoneCode: 'Надіслати SMS на {{identifier}}',
    blockButton__totp: 'Використати застосунок-автентифікатор',
    getHelp: {
      blockButton__emailSupport: 'Написати на підтримку',
      content: 'Якщо у вас виникли труднощі з входом, зверніться до служби підтримки.',
      title: 'Отримати допомогу',
    },
    subtitle: 'Виберіть один з доступних методів для підтвердження вашої особи.',
    title: 'Використати інший метод',
  },

  // Account selection
  signIn_accountSwitcher: {
    title: 'Виберіть акаунт',
    subtitle: 'щоб продовжити до {{applicationName}}',
    action__addAccount: 'Додати акаунт',
    action__signOutAll: 'Вийти з усіх акаунтів',
    badge__lastUsed: 'Останній використаний',
    badge__preferred: 'Рекомендований',
    lastUsed: 'Останній використаний',
  },

  // Last used label migrated to Badges section

  // Help texts
  signIn_getHelp: {
    title: 'Отримати допомогу',
    content: "Якщо у вас виникли труднощі з входом до вашого акаунту, зв'яжіться з нами, і ми допоможемо вам відновити доступ.",
    blockButton__emailSupport: 'Написати на підтримку',
  },
};
