export const ukUA = {
  // Social buttons
  socialButtonsBlockButton: 'Продовжити через {{provider|titleize}}',

  // Sign In
  signIn: {
    start: {
      title: 'Вхід',
      subtitle: 'Увійдіть до вашого акаунту',
      actionText: 'Не маєте акаунту?',
      actionLink: 'Зареєструватися',
    },
    emailLink: {
      title: 'Перевірте вашу пошту',
      subtitle: 'Ми надіслали посилання для входу на {{emailAddress}}',
      formTitle: 'Посилання для входу',
      formSubtitle: 'Використайте посилання, надіслане на вашу пошту',
      resendButton: 'Не отримали посилання? Надіслати знову',
    },
    password: {
      title: 'Введіть пароль',
      subtitle: 'для продовження до {{applicationName}}',
      actionLink: 'Використати інший метод',
      forgotPasswordLink: 'Забули пароль?',
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
      actionLink: 'Отримати допомогу',
      blockButton__emailCode: 'Надіслати код на {{identifier}}',
      blockButton__emailLink: 'Надіслати посилання на {{identifier}}',
      blockButton__password: 'Увійти з паролем',
      blockButton__phoneCode: 'Надіслати SMS код на {{identifier}}',
      blockButton__totp: 'Використати код автентифікатора',
    },
    forgotPassword: {
      title: 'Скинути пароль',
      subtitle: 'Введіть вашу електронну пошту',
      formTitle: 'Код скидання пароля',
      formSubtitle: 'Введіть код, надісланий на вашу пошту',
      resendButton: 'Не отримали код? Надіслати знову',
    },
  },

  // Sign Up
  signUp: {
    start: {
      title: 'Реєстрація',
      subtitle: 'Створіть новий акаунт',
      actionText: 'Вже маєте акаунт?',
      actionLink: 'Увійти',
    },
    emailLink: {
      title: 'Перевірте вашу пошту',
      subtitle: 'Ми надіслали посилання для верифікації на {{emailAddress}}',
      formTitle: 'Посилання для верифікації',
      formSubtitle: 'Використайте посилання, надіслане на вашу пошту',
      resendButton: 'Не отримали посилання? Надіслати знову',
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

  // Form fields
  formFieldLabel__emailAddress: 'Електронна пошта',
  formFieldLabel__emailAddress_username: 'Електронна пошта або ім\'я користувача',
  formFieldLabel__username: 'Ім\'я користувача',
  formFieldLabel__phoneNumber: 'Номер телефону',
  formFieldLabel__password: 'Пароль',
  formFieldLabel__newPassword: 'Новий пароль',
  formFieldLabel__confirmPassword: 'Підтвердіть пароль',
  formFieldLabel__signOutOfOtherSessions: 'Вийти з інших пристроїв',
  formFieldLabel__firstName: 'Ім\'я',
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
  formFieldInputPlaceholder__emailAddress_username: 'name@example.com або ім\'я користувача',
  formFieldInputPlaceholder__emailAddresses: 'name@example.com, name2@example.com',
  formFieldInputPlaceholder__phoneNumber: '+380',
  formFieldInputPlaceholder__username: 'ім\'я_користувача',
  formFieldInputPlaceholder__firstName: 'Ваше ім\'я',
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

  // Form field hints
  formFieldHintText__optional: 'Необов\'язково',
  formFieldHintText__slug: 'Ідентифікатор - це читабельний ID, який має бути унікальним. Часто використовується в URL.',

  // Form buttons
  formButtonPrimary: 'Продовжити',
  formButtonPrimary__verify: 'Підтвердити',

  // Footer
  footerActionLink__useAnotherMethod: 'Використати інший метод',
  footerActionLink__help: 'Потрібна допомога?',
  footerActionLink__privacy: 'Конфіденційність',
  footerActionLink__terms: 'Умови',

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

  // Errors
  unstable__errors: {
    form_identifier_not_found: '',
    form_password_pwned: 'Цей пароль був скомпрометований в результаті витоку даних і не може бути використаний. Будь ласка, спробуйте інший пароль.',
    form_username_invalid_length: '',
    form_username_invalid_character: '',
    form_param_format_invalid: '',
    form_param_format_invalid__email_address: 'Електронна адреса має бути дійсною адресою електронної пошти.',
    form_param_format_invalid__phone_number: 'Номер телефону має бути у дійсному міжнародному форматі',
    form_param_nil: '',
    form_password_length_too_short: '',
    form_param_max_length_exceeded__name: '',
    form_password_not_strong_enough: 'Ваш пароль недостатньо надійний.',
    form_password_size_in_bytes_exceeded: 'Ваш пароль перевищив максимальну кількість байтів, будь ласка, скоротіть його або видаліть деякі спеціальні символи.',
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
        userInputs: 'Не повинно бути жодних особистих або пов\'язаних зі сторінкою даних.',
        pwned: 'Ваш пароль був розкритий в результаті витоку даних в Інтернеті.',
      },
      suggestions: {
        l33t: 'Уникайте передбачуваних замін літер, таких як "@" замість "a".',
        reverseWords: 'Уникайте зворотного написання поширених слів.',
        allUppercase: 'Використовуйте великі літери для деяких, але не всіх літер.',
        capitalization: 'Використовуйте великі літери не тільки для першої літери.',
        dates: 'Уникайте дат та років, пов\'язаних з вами.',
        recentYears: 'Уникайте останніх років.',
        associatedYears: 'Уникайте років, пов\'язаних з вами.',
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
}
