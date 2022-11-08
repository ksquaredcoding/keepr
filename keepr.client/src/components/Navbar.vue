<template>
  <div class="row justify-content-between align-items-center border-bottom attempt">
    <div class="col-2 d-flex justify-content-evenly">
      <router-link class="d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <button class="btn home-button">Home</button>
        </div>
      </router-link>
      <div class="dropdown">
        <a class="dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </a>

        <ul class="dropdown-menu">
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#keepForm"
              style="z-index: 10">Keep</a></li>
          <li><a class="dropdown-item" href="#" style="z-index: 10">Vault</a></li>
        </ul>
      </div>
    </div>
    <div class="col-2">
      <img class="logo-img" src="src\assets\img\keeprlogo.png" alt="keepr co" title="keepr co">
    </div>
    <div class="col-1 me-5">
      <Login />
    </div>
  </div>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped>
.home-button {
  border-radius: 15px;
  background-color: #E9D8D6;
  color: #2D2D2D;
  font-weight: bold;
  letter-spacing: 0.03rem;
}

.logo-img {
  height: auto;
  width: 8vw;
  object-fit: cover;
  padding: 1rem;
}

.attempt {
  overflow: visible;
  z-index: 999;
}
</style>
