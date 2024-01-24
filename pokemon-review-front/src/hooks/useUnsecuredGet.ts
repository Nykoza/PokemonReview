import { baseUrl } from '@/models/auth'

export const useUnsecuredGet = (url: string): Promise<Response> => {
  return fetch(baseUrl + url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

export const useUnsecuredPost = <T>(url: string, body: T, searchParams?: URLSearchParams) => {
  return fetch(baseUrl + url + `?${searchParams}`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(body)
  })
}
