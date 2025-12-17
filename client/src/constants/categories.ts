export const CategoryConstants = {
  Roads: 'Дороги',
  Lighting: 'Освітлення',
  Garbage: 'Сміття',
  Water: 'Водопостачання',
  PublicTransport: 'Громадський транспорт',
  Parks: 'Парки та зелені зони',
  Safety: 'Безпека',
  Noise: 'Шум',
  Animals: 'Тварини',
  Other: 'Інше',
} as const

export type CategoryType = (typeof CategoryConstants)[keyof typeof CategoryConstants]

export const CATEGORIES: { value: string; label: string }[] = [
  { value: CategoryConstants.Roads, label: 'Дороги' },
  { value: CategoryConstants.Lighting, label: 'Освітлення' },
  { value: CategoryConstants.Garbage, label: 'Сміття' },
  { value: CategoryConstants.Water, label: 'Водопостачання' },
  { value: CategoryConstants.PublicTransport, label: 'Громадський транспорт' },
  { value: CategoryConstants.Parks, label: 'Парки та зелені зони' },
  { value: CategoryConstants.Safety, label: 'Безпека' },
  { value: CategoryConstants.Noise, label: 'Шум' },
  { value: CategoryConstants.Animals, label: 'Тварини' },
  { value: CategoryConstants.Other, label: 'Інше' },
]

export const getAllCategoryNames = (): string[] => CATEGORIES.map((c) => c.value)
