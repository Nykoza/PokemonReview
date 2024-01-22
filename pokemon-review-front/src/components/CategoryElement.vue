<script setup lang="ts">
import type { Category } from '@/models/category'

interface Props {
  category: Category
}

defineProps<Props>()
</script>

<template>
  <div>
    <v-img src="./assets/pikachu.png" />
    <v-card-title class="text">{{ category.name }}</v-card-title>
  </div>
</template>

<style scoped>
.text {
  color: #55828b;
}
</style>
