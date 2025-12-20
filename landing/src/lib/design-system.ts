export const designSystem = {
  colors: {
    header: {
      background: '#F7F9FD',
      text: '#000000',
      shadow: 'rgba(0, 0, 0, 0.25)',
    },
    footer: {
      background: '#1F2732',
      text: '#FFFFFF',
      textSecondary: '#9CA3AF',
      accent: '#D81B60',
      elementBackground: '#3A3F47',
    },
  },
  
  borderRadius: {
    floating: '30px',
    card: '30px',
  },
  
  shadows: {
    floating: '0 4px 12px rgba(0, 0, 0, 0.25)',
  },
  
  typography: {
    fontFamily: {
      heading: 'var(--font-heading)',
      body: 'var(--font-sans)',
    },
    weights: {
      regular: '400',
      semibold: '600',
      bold: '700',
    },
  },
  
  spacing: {
    container: {
      padding: '1rem',
      maxWidth: '1280px',
    },
  },
} as const

export type DesignSystem = typeof designSystem
