export const designSystem = {
  colors: {
    page: {
      background: '#FFFFFF',
    },
    header: {
      background: '#F7F9FD',
      text: '#000000',
      shadow: 'rgba(0, 0, 0, 0.25)',
    },
    hero: {
      background: '#FAFBFD',
      backgroundGradient: 'linear-gradient(135deg, #FFFFFF 0%, #F0F4F8 50%, #E8F0F7 100%)',
      headingText: 'rgba(26, 26, 26, 0.8)', // fill 80%
      bodyText: 'rgba(74, 85, 104, 0.8)', // fill 80%
      ctaButton: 'rgba(228, 37, 86, 0.8)', // #E42556 with 80% opacity
      ctaButtonHover: 'rgba(228, 37, 86, 0.9)', // slightly more opaque on hover
      ctaButtonText: '#EAEAEA',
      ctaButtonShadow: '0 4px 8px rgba(228, 37, 86, 0.3)', // Y:4, blur:8
    },
    partnerships: {
      sectionTitle: '#1F2732',
      cardBackground: '#FFFFFF',
      cardTitle: '#000000',
      cardDescription: '#000000',
      iconColor: '#1F2732',
      buttonBackground: '#1F2732',
      buttonText: '#FFFFFF',
    },
    services: {
      eyebrowText: '#D44374', // Насичений рожевий
      headingText: '#1F2732', // Темно-сірий
      cardBackground: '#F0F1F2', // Світло-сірий
      cardTitle: '#1F2732', // Темно-сірий
      linkText: '#596872', // Приглушений сіро-блакитний
      linkIcon: '#596872', // Приглушений сіро-блакитний
    },
    profile: {
      headerBackground: '#F7F9FA', // Світло-сірий фон для верхньої панелі
      headingText: '#1F2732', // Темно-сірий для заголовків
      accentPink: '#E42556', // Рожевий акцент
      inputBackground: '#F0F1F2', // Світло-сірий фон для полів вводу
      cardBackground: '#FFFFFF', // Білий фон для карток
      warningRed: '#DC2626', // Яскраво-червоний для небезпечних дій
      tabs: {
        background: '#F8F9FB',
        activeBackground: '#FFFFFF',
        border: '#D9DEE5',
        activeBorder: '#C4C9D1',
        text: '#1F2732',
        inactiveText: '#596872',
        indicator: '#E42556',
        hoverText: '#1F2732',
      },
      links: {
        text: '#596872',
        hover: '#1F2732',
        disabled: '#B9C1CC',
      },
    },
  },

  borderRadius: {
    floating: '30px',
    card: '30px',
    button: '20px',
  },

  shadows: {
    floating: '0 4px 12px rgba(0, 0, 0, 0.25)',
    button: '0 4px 8px rgba(228, 37, 86, 0.3)',
    buttonHover: '0 4px 8px rgba(228, 37, 86, 0.5)',
    card: '0 4px 10px 3px rgba(0, 0, 0, 0.08)', // Y:4, blur:10, spread:3
  },

  typography: {
    fontFamily: {
      heading: 'var(--font-heading)',
      body: 'var(--font-sans)',
    },
    weights: {
      regular: '400',
      medium: '500',
      semibold: '600',
      bold: '700',
      extrabold: '800',
    },
  },

  spacing: {
    container: {
      padding: '1rem',
      maxWidth: '1280px',
    },
  },
} as const;

export type DesignSystem = typeof designSystem;
