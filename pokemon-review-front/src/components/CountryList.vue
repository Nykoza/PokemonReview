<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import { useUnsecuredGet } from '@/hooks/useUnsecuredGet'
import type { Country } from '@/models/country'
import CountryElement from '@/components/CountryElement.vue'

const countries = ref<Country[]>([])

onBeforeMount(() => {
  useUnsecuredGet('/api/country')
    .then((res) => res.json())
    .then((data: Country[]) => {
      countries.value = data
    })
    .catch((err) => console.error(err))
})
</script>

<template>
  <CountryElement :country="country" v-for="country in countries" :key="country" />
</template>

<style scoped></style>
