<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import type { Pokemon } from '@/models/pokemon'
import { useUnsecuredGet } from '@/hooks/useUnsecuredGet'
import PokemonElement from '@/components/PokemonElement.vue'

const pokemons = ref<Pokemon[]>([])

onBeforeMount(() => {
  useUnsecuredGet('/api/pokemon')
    .then((res) => res.json())
    .then((pokemonData: Pokemon[]) => {
      pokemons.value = pokemonData
    })
    .catch((err) => console.error(err))
})
</script>

<template>
  <v-container class="list-border">
    <PokemonElement v-for="pokemon in pokemons" :key="pokemon" :pokemon="pokemon" />
  </v-container>
</template>

<style scoped></style>
