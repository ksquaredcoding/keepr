<template>
  <header class="no-x-over body-bg-color">
    <Navbar />
  </header>
  <main class="no-x-over body-bg-color">
    <router-view />
  </main>
  <KeepModal />
  <KeepForm />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepForm from "./components/KeepForm.vue"
import KeepModal from "./components/KeepModal.vue"
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, KeepForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

// header {
//   max-height: 10vh;
// }
</style>
