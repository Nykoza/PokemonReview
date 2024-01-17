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
  <PokemonElement v-for="pokemon in pokemons" :key="pokemon" :pokemon="pokemon" />
</template>

<style scoped></style>
