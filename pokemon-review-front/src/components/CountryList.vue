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
  <v-container class="list-border">
    <CountryElement :country="country" v-for="country in countries" :key="country" />
  </v-container>
</template>

<style scoped></style>
