<template>
  <div class="row justify-content-between align-items-center border-bottom">
    <div class="col-2 d-flex justify-content-evenly">
      <router-link class="d-flex" :to="{ name: 'Home' }">
        <div class="d-flex flex-column align-items-center">
          <button class="btn home-button">Home</button>
        </div>
      </router-link>
      <h5>Create v</h5>
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
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

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
</style>
