<script setup lang="ts">
import { onBeforeMount, ref } from 'vue'
import { useUnsecuredGet } from '@/hooks/useUnsecuredGet'
import type { Owner } from '@/models/owner'
import OwnerElement from '@/components/OwnerElement.vue'
import router from '@/router'

const owners = ref<Owner[]>()

onBeforeMount(() => {
  useUnsecuredGet('/api/owner')
    .then((res) => res.json())
    .then((data: Owner[]) => {
      owners.value = data
    })
    .catch((err) => console.error(err))
})
</script>

<template>
  <v-container class="list-border">
    <OwnerElement :owner="owner" v-for="owner in owners" :key="owner" />

    <button @click="router.push('owners/create')">Add Owner</button>
  </v-container>
</template>

<style scoped></style>
