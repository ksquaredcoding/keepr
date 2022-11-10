<template>
  <div class="d-flex justify-content-between align-items-center body-bg-color border-top">
    <div>
      <router-link :to="{ name: 'Home' }">
        <img class="selectable logo-img my-2 ms-2" src="src\assets\img\keeprlogo.png" alt="Return Home"
          title="Return Home">
      </router-link>
    </div>
    <div class="nav-item dropdown dropup" v-if="account.id">
      <button class="btn text-truncate dropdown-toggle fw-bold fs-5" type="button" data-bs-toggle="dropdown"
        aria-expanded="false">
        Create
      </button>
      <ul class="dropdown-menu dropdown-menu-lg-right">
        <li><a class=" dropdown-item" data-bs-toggle="modal" data-bs-target="#keepForm">Keep</a>
        </li>
        <li><a class="dropdown-item" data-bs-toggle="modal" data-bs-target="#vaultForm">Vault</a></li>
      </ul>
    </div>
    <Login class="me-2" />
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState.js";
import Login from "./Login.vue";

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: { Login }
}
</script>


<style lang="scss" scoped>
.logo-img {
  width: 5em;
  height: 5em;
}
</style>