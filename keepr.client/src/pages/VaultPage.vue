<template>
  <div class="container-fluid" v-if="vault">
    <div class="d-flex justify-content-center">
      <div class="d-flex justify-content-end flex-column align-items-center vault-cover">
        <div>
          <h1 class="vault-text">{{ vault?.name }}</h1>
        </div>
        <div>
          <p class="vc-text"><b>by {{ vault?.creator?.name }}</b></p>
        </div>
      </div>
    </div>
    <div class="d-flex justify-content-around" v-if="account?.id == vault?.creatorId">
      <div></div>
      <div class="dropdown dropend">
        <button class="btn text-truncate dropdown-toggle fw-bold fs-5" type="button" data-bs-toggle="dropdown"
          aria-expanded="false" title="Vault Options">
          ...
        </button>
        <ul class="dropdown-menu dropdown-menu-lg-right">
          <li><a class=" dropdown-item" data-bs-toggle="modal" data-bs-target="#editVaultForm" title="Edit Vault">Edit
              Vault</a>
          </li>
          <li><a class="dropdown-item text-danger" @click="removeVault()" title="Delete Vault">Delete Vault</a></li>
        </ul>
      </div>
    </div>
    <div class="d-flex justify-content-center">
      <div>
        <p class="keepnum my-2"><b>{{ keeps.length }} Keeps</b></p>
      </div>
    </div>
    <div class="row ms-1 me-2 mt-1 g-4" v-masonry transition-duration="0.3s" item-selector=".item">
      <KeepCard v-for="k in keeps" :keep="k" :key="k.id" v-masonry-tile class="item" />
    </div>
  </div>
  <div class="d-flex text-center" v-else>
    <h1>Loading...</h1>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { onMounted } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const route = useRoute()
    async function getActiveVault() {
      try {
        await vaultsService.getActiveVault(route.params.id)
      } catch (error) {
        console.error('[GETTING ACTIVE VAULT]', error)
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      getActiveVault()
    })
    return {
      route,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultkeeps),
      coverImg: computed(() => `url(${AppState.activeVault?.img})`),
      async removeVault() {
        try {
          const yes = await Pop.confirm("Are you sure you want to delete this vault?")
          if (!yes) {
            return
          }
          await vaultsService.removeVault(route.params.id)
          Pop.success("You have deleted this vault")
          return
        } catch (error) {
          console.error('[DELETING VAULT]', error)
          Pop.error(error.message)
        }
      }
    };
  },
  components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.vault-cover {
  background-image: v-bind(coverImg);
  width: 70vw;
  height: 30vh;
  border-radius: 8px;
  margin-top: 1rem;
  background-size: cover;
  background-position: center;
  overflow: hidden;
}

.vc-text {
  color: #FEF6F0;
  text-shadow: 0px 6px 6px rgba(0, 0, 0, 0.45);
  letter-spacing: 0.05em;
}

.keepnum {
  background-color: #DED6E9;
  border-radius: 15px;
  width: 6vw;
  height: 3vh;
  text-align: center;
}
</style>