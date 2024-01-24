<script setup lang="ts">
import { ref } from 'vue'

interface Props {
  snackbarStatus: boolean
}

const props = defineProps<Props>()
const snackbar = ref(props.snackbarStatus)
</script>

<template>
  <v-snackbar v-model="snackbar">
    <template v-slot:actions>
      <v-btn variant="text" color="blue" @click="snackbar = false">Close me</v-btn>
    </template>
  </v-snackbar>
</template>

<style scoped></style>
