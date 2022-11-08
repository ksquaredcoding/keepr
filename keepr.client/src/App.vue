<template>
  <header class="no-x-over body-bg-color" style="z-index: 10">
    <Navbar2 />
  </header>
  <main class="no-x-over body-bg-color" style="z-index: 9">
    <router-view />
  </main>
  <KeepModal />
  <KeepForm />
  <VaultForm />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepForm from "./components/KeepForm.vue"
import VaultForm from "./components/VaultForm.vue"
import KeepModal from "./components/KeepModal.vue"
import Navbar from './components/Navbar.vue'
import Navbar2 from "./components/Navbar2.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, KeepForm, Navbar2 }
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
