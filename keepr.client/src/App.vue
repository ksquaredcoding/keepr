<template>
  <header class="no-x-over body-bg-color" style="z-index: 10">
    <Navbar2 class="d-none d-lg-block" />
  </header>
  <main class="no-x-over body-bg-color footer-margin" style="z-index: 9">
    <router-view />
  </main>
  <KeepModal />
  <KeepForm />
  <VaultForm />
  <EditVaultForm />
  <EditAccountForm />
  <footer class="body-bg-color">
    <div class="row mt-2 d-none d-lg-block"></div>
    <NavbarMobile class="d-lg-none d-block fixed-bottom" />
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepForm from "./components/KeepForm.vue"
import VaultForm from "./components/VaultForm.vue"
import KeepModal from "./components/KeepModal.vue"
import Navbar from './components/Navbar.vue'
import Navbar2 from "./components/Navbar2.vue"
import EditVaultForm from "./components/EditVaultForm.vue"
import EditAccountForm from "./components/EditAccountForm.vue"
import NavbarMobile from "./components/NavbarMobile.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, KeepForm, Navbar2, EditVaultForm, EditAccountForm, NavbarMobile }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

@media (max-width: 1200px) {
  .footer-margin {
    margin-bottom: 98px;
  }
}
</style>
