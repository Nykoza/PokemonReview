<script setup lang="ts">
import type { Owner } from '@/models/owner'

interface Props {
  owner: Owner
}

defineProps<Props>()
</script>

<template>
  <div>
    <v-img src="./assets/pikachu.png" />
    <v-card-title class="text">{{ owner.name }}</v-card-title>
    <v-card-subtitle class="text">Work at {{ owner.gym }}</v-card-subtitle>
  </div>
</template>

<style scoped>
.text {
  color: #55828b;
}
</style>
