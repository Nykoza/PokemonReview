<script setup lang="ts">
import PokemonList from '@/components/PokemonList.vue'
import OwnerList from '@/components/OwnerList.vue'
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12" v-for="n in 4" :key="n" sm="4">
        <PokemonList />
        <OwnerList />
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped></style>
