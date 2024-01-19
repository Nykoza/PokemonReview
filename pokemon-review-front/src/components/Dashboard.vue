<script setup lang="ts">
import PokemonList from '@/components/PokemonList.vue'
import OwnerList from '@/components/OwnerList.vue'
import CountryList from '@/components/CountryList.vue'
import CategoryList from '@/components/CategoryList.vue'
</script>

<template>
  <v-container class="w-100 bg-surface-variant">
    <v-row>
      <v-col>
        <PokemonList />
      </v-col>
      <v-col>
        <OwnerList />
      </v-col>
      <v-col>
        <CountryList />
      </v-col>
      <v-col>
        <CategoryList />
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped></style>
