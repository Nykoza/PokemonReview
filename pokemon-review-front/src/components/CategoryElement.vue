<script setup lang="ts">
import type { Category } from '@/models/category'

interface Props {
  category: Category
}

defineProps<Props>()
</script>

<template>
  <v-card>
    <v-img src="./assets/pikachu.png" />
    <v-card-title>{{ category.name }}</v-card-title>
  </v-card>
</template>

<style scoped></style>
