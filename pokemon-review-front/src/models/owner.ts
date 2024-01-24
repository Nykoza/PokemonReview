import type { Country } from '@/models/country'

export interface Owner {
  id: number
  name: string
  gym: string
  country: Country
}

export interface OwnerCreatePayload {
  name: string
  gym: string
}
