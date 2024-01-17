<script setup lang="ts">
import type { Owner } from '@/models/owner'

interface Props {
  owner: Owner
}

defineProps<Props>()
</script>

<template>
  <v-card>
    <v-img src="./assets/pikachu.png" />
    <v-card-title>{{ owner.name }}</v-card-title>
    <v-card-subtitle>Born the {{ owner.gym }}</v-card-subtitle>
  </v-card>
</template>

<style scoped></style>
