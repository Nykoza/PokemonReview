<script setup lang="ts">
import type { Pokemon } from '@/models/pokemon'

interface Props {
  pokemon: Pokemon
}

defineProps<Props>()
</script>

<template>
  <v-card>
    <v-img src="./assets/pikachu.png" />
    <v-card-title>{{ pokemon.name }}</v-card-title>
    <v-card-subtitle>Born the {{ pokemon.birthdate }}</v-card-subtitle>
  </v-card>
</template>

<style scoped></style>
