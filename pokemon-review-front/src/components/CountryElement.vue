<script setup lang="ts">
import type { Country } from '@/models/country'

interface Props {
  country: Country
}

defineProps<Props>()
</script>

<template>
  <v-card>
    <v-img src="./assets/pikachu.png"></v-img>
    <v-card-title>{{ country.name }}</v-card-title>
  </v-card>
</template>

<style scoped></style>
