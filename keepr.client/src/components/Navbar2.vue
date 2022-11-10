<template>
  <nav class="navbar navbar-expand-lg bg-light ">
    <div class="container-fluid row mx-2 ">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0 col-12 d-flex justify-content-between">
        <li class="d-flex flex-nowrap align-items-center ms-2">
          <div class="nav-item">
            <router-link class="d-flex" :to="{ name: 'Home' }">
              <button class="btn home-button fw-bold fs-5" type="button" title="Return Home">Home</button>
            </router-link>
          </div>
          <div class="nav-item dropdown dropend" v-if="account.id">
            <button class="btn text-truncate dropdown-toggle fw-bold fs-5" type="button" data-bs-toggle="dropdown"
              aria-expanded="false" title="Create a keep or vault">
              Create
            </button>
            <ul class="dropdown-menu dropdown-menu-lg-right">
              <li><a class=" dropdown-item" data-bs-toggle="modal" data-bs-target="#keepForm"
                  title="Create a keep">Keep</a>
              </li>
              <li><a class="dropdown-item" data-bs-toggle="modal" data-bs-target="#vaultForm"
                  title="Create a vault">Vault</a></li>
            </ul>
          </div>
        </li>
        <li>
          <a class="navbar-brand" href="#"><img class="logo-img" src="src\assets\img\keeprlogo.png"
              alt="keepr co/return home" title="keepr co"></a>
        </li>
        <li class="nav-item d-flex flex-nowrap align-items-center">
          <Login />
        </li>
      </ul>
    </div>
  </nav>
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
.home-button {
  border-radius: 15px;
  background-color: #E9D8D6;
  color: #2D2D2D;
  font-weight: bold;
  letter-spacing: 0.03rem;
}

.logo-img {
  height: 10vh;
  width: auto;
  padding: 1rem;
}

.y-over {
  overflow: auto;
  z-index: 999;
}

.no-y-over {
  overflow-y: hidden;
}
</style>