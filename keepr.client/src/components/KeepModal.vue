<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body" v-if="keep">
          <div class="row animate_animated animate_fadeIn">
            <div class="col-md-4">
              <img class="img-fluid rounded" :src="keep?.img" :alt="keep?.name">
            </div>
            <div class="col-md-8 d-flex flex-column justify-content-between">
              <div class="d-flex justify-content-center">
                <p class="mx-2 fs-5"><i class="mdi mdi-eye-outline"></i> {{ keep?.views }}</p>
                <p class="mx-2 fs-5"><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep?.kept }}</p>
              </div>
              <div class="row justify-content-center">
                <div>
                  <h1 class="text-center">{{ keep?.name }}</h1>
                </div>
                <div class="d-flex justify-content-center">
                  <p>{{ keep?.description }}</p>
                </div>
              </div>
              <div class="d-flex justify-content-between">
                <div v-if="activeVault?.creatorId == account?.id && route.name == 'Vault'">
                  <button class="btn" @click="removeVaultKeep(keep?.id)">Remove</button>
                </div>
                <div v-else-if="keep?.creatorId == account?.id && route.name == 'Account'">
                  <button class="btn" @click="removeMyKeep(keep?.id)">Remove</button>
                </div>
                <div class="d-flex" v-else>
                  <form @submit.prevent="handleSubmit">
                    <select class="btn border-0 fw-bold me-2" name="vaultId" aria-label="Select a vault to save keep to"
                      v-model="editable.vaultId">
                      <option selected>Add to Vault</option>
                      <option :value="v.id" v-for="v in vaults" :key="v.id">{{ v.name }}</option>
                    </select>
                    <button class="btn btn-dark ms-2" style="height: fit-content;">save</button>
                  </form>
                </div>
                <div class="d-flex align-items-center">
                  <router-link :to="{ name: 'Profile', params: { id: keep?.creator?.id } }">
                    <img class="creator-pic mx-1" :src="keep?.creator?.picture" :alt="keep?.creator?.name"
                      :title="keep?.creator?.name" data-bs-dismiss="modal">
                  </router-link>
                  <p class="mx-1 mt-2"><strong>{{ keep?.creator?.name }}</strong></p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { Keep } from "../models/Keep.js";
import { AppState } from "../AppState.js"
import Pop from "../utils/Pop.js";
import { onMounted, watchEffect, ref } from "vue";
import { accountService } from "../services/AccountService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const route = useRoute()
    const editable = ref({})
    async function getMyVaults() {
      try {
        const vaults = await accountService.getMyVaults();
        return vaults;
      }
      catch (error) {
        console.error("[GETTING ACCOUNT VAULTS]", error);
        Pop.error(error.message);
      }
    }
    watchEffect(() => {
      if (AppState.account.id) {
        getMyVaults();
      }
    });
    return {
      activeVault: computed(() => AppState.activeVault),
      route,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      editable,
      vaults: computed(() => AppState.vaults),
      async handleSubmit() {
        try {
          await vaultKeepsService.addVaultKeep(editable.value)
          Pop.success("You've added this keep to your vault!")
        } catch (error) {
          console.error('[SAVING KEEP TO VAULT]', error)
          Pop.error(error.message)
        }
      },
      async removeVaultKeep(keepId) {
        try {
          const yes = await Pop.confirm("Remove keep from this vault?")
          if (!yes) {
            return
          }
          await vaultKeepsService.removeVaultKeep(keepId, route.params.id)
          Modal.getInstance("#keepModal").hide()
          Pop.success("You have removed keep from this vault")
        } catch (error) {
          console.error('[REMOVING VAULTKEEP]', error)
          Pop.error(error.message)
        }
      },
      async removeMyKeep(keepId) {
        try {
          const yes = await Pop.confirm("Delete your created keep?")
          if (!yes) {
            return
          }
          await keepsService.removeMyKeep(keepId)
          Modal.getInstance("#keepModal").hide()
          Pop.success("You have removed this keep")
        } catch (error) {
          console.error('[REMOVING KEEP]', error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.creator-pic {
  border-radius: 50%;
  height: 2.5rem;
  width: 2.5rem;
}
</style>