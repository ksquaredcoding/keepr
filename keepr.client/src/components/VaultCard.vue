<template>
  <div
    class="col-md-3 rounded m-2 vault-card d-flex flex-column justify-content-end selectable animate_animated animate_fadeIn"
    @click="getActiveVault(vault?.id)" :title="vault?.name">
    <router-link :to="{ name: 'Vault', params: { id: vault?.id } }">
      <div class="d-flex justify-content-between">
        <h4 class="vault-text text-uppercase">{{ vault?.name }}</h4>
        <i class="bi bi-lock-fill lock-bg text-dark" v-if="vault?.isPrivate && route.name == 'Account'"></i>
      </div>
    </router-link>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    vault: {
      type: Vault,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      coverImg: computed(() => `url(${props.vault?.img})`),
      async getActiveVault(vaultId) {
        try {
          await vaultsService.getActiveVault(vaultId)
        } catch (error) {
          console.error('[GETTING ACTIVE VAULT]', error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.vault-card {
  height: 7rem;
  width: 10rem;
  background-image: v-bind(coverImg);
  background-size: cover;
  background-position: center;
  overflow: hidden;
  position: relative;
}

.lock-bg {
  background-color: #eaeaea;
  border-radius: 50%;
  height: fit-content;
  width: fit-content;
}
</style>