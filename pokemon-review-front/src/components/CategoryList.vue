<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import { useUnsecuredGet } from '@/hooks/useUnsecuredGet'
import type { Category } from '@/models/category'
import CategoryElement from '@/components/CategoryElement.vue'

const categories = ref<Category[]>([])

onBeforeMount(() => {
  useUnsecuredGet('/api/category')
    .then((res) => res.json())
    .then((data: Category[]) => {
      categories.value = data
    })
    .catch((err) => console.error(err))
})
</script>

<template>
  <CategoryElement :category="category" v-for="category in categories" :key="category" />
</template>

<style scoped></style>
