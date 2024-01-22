<script setup lang="ts">
import type { Pokemon } from '@/models/pokemon'

interface Props {
  pokemon: Pokemon
}

defineProps<Props>()
</script>

<template>
  <div>
    <v-img src="./assets/pikachu.png" />
    <v-card-title class="text">{{ pokemon.name }}</v-card-title>
    <v-card-subtitle class="text"
      >Born the {{ new Date(pokemon.birthDate).toDateString() }}</v-card-subtitle
    >
  </div>
</template>

<style scoped>
.text {
  color: #55828b;
}
</style>
