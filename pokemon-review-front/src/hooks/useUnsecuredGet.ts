import { baseUrl } from '@/models/auth'

export const useUnsecuredGet = (url: string): Promise<Response> => {
  return fetch(baseUrl + url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

export const useUnsecuredPost = <T>(url: string, body: T) => {
  return fetch(baseUrl + url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(body)
  })
}
