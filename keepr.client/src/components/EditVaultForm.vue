<template>
  <div class="modal fade" id="editVaultForm" tabindex="-1" aria-labelledby="editVaultFormLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="col-lg-6">
                <div>
                  <img :src="editable.img" alt="vault image preview"
                    class="mt-2 preview-img animate_animated animate_fadeIn">
                </div>
                <div class="form-group">
                  <label for="img">Image:</label>
                  <input type="url" v-model="editable.img" required class="form-control"
                    placeholder="Give your vault a cool image">
                </div>
                <div class="form-check">
                  <label class="form-check-label" for="isPrivate">
                    Private Vault?
                  </label>
                  <input class="form-check-input" type="checkbox" v-model="editable.isPrivate" id="isPrivate">
                </div>
              </div>
              <div class="col-lg-6">
                <div class="form-group">
                  <label for="name"> Name:</label>
                  <input type="text" v-model="editable.name" placeholder="Name of vault" required class="form-control">
                </div>
                <div class="form-group">
                  <label for="description">Description:</label>
                  <textarea v-model="editable.description" placeholder="Tell us about your vault..."
                    class="form-control" rows="4" required></textarea>
                </div>
              </div>
              <div class="my-3">
                <button class="btn btn-primary" type="submit" title="Edit vault">Edit Vault
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref, watchEffect } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js"
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.activeVault }
    })
    return {
      editable,
      async handleSubmit() {
        try {
          await vaultsService.editVault(editable.value)
          Modal.getInstance("#editVaultForm").hide()
          Pop.success("You have successfully edited this vault")
        } catch (error) {
          console.error('[EDITING VAULT]', error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.preview-img {
  border-radius: 5%;
  border: dashed 4px #ababab;
  height: 15rem;
  width: 30rem;
  object-fit: cover;
}
</style>