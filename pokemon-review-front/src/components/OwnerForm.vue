<script setup lang="ts">
import { ref } from 'vue'
import { useUnsecuredPost } from '@/hooks/useUnsecuredGet'
import router from '@/router'

const ownerName = ref<string>('')
const gymName = ref<string>('')

const submit = async () => {
  const searchParams = new URLSearchParams()
  searchParams.append('countryId', '1')
  const result = await useUnsecuredPost(
    '/api/owner',
    { name: ownerName.value, gym: gymName.value },
    searchParams
  )
  console.log(result)
  if (result.ok) {
    router.push('/')
  }
}
</script>

<template>
  <v-container>
    <v-form class="d-flex ga-5 flex-column" @submit.prevent="submit">
      <v-label class="text">Create owner</v-label>
      <div>
        <v-label class="text">Owner name:</v-label>
        <v-text-field class="text" v-model="ownerName" />
      </div>
      <div>
        <v-label class="text">Gym name:</v-label>
        <v-text-field class="text" v-model="gymName" />
      </div>
      <v-btn class="submit-button" type="submit">Create owner</v-btn>
    </v-form>
  </v-container>
</template>

<style scoped>
.text {
  color: #55828b;
}
.submit-button {
  background-color: #eef5db;
}
</style>
