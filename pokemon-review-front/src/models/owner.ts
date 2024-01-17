import type { Country } from '@/models/country'

export interface Owner {
  id: number
  name: string
  gym: string
  country: Country
}
