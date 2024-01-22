<script setup lang="ts">
import type { Country } from '@/models/country'

interface Props {
  country: Country
}

defineProps<Props>()
</script>

<template>
  <div>
    <v-img src="./assets/pikachu.png"></v-img>
    <v-card-title class="text">{{ country.name }}</v-card-title>
  </div>
</template>

<style scoped>
.text {
  color: #55828b;
}
</style>
