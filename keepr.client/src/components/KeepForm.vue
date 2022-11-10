<template>
  <div class="modal fade" id="keepForm" tabindex="-1" aria-labelledby="keepFormLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="col-lg-6">
                <div>
                  <img :src="editable.img" alt="keep image preview"
                    class="mt-2 preview-img animate_animated animate_fadeIn">
                </div>
                <div class="form-group">
                  <label for="img">Image:</label>
                  <input type="url" v-model="editable.img" required class="form-control"
                    placeholder="Give your keep a cool image">
                </div>
              </div>
              <div class="col-lg-6">
                <div class="form-group">
                  <label for="name"> Name:</label>
                  <input type="text" v-model="editable.name" placeholder="Name of keep" required class="form-control">
                </div>
                <div class="form-group">
                  <label for="description">Description:</label>
                  <textarea v-model="editable.description" placeholder="Tell us about your keep..." class="form-control"
                    rows="4" required></textarea>
                </div>
              </div>
              <div class="my-3">
                <button class="btn btn-primary" type="submit" title="Create Keep">Create Keep
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
import { Modal } from "bootstrap";
import { ref } from "vue";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handleSubmit() {
        try {
          await keepsService.createKeep(editable.value)
          editable.value = {}
          Modal.getInstance("#keepForm").hide()
          Modal.getOrCreateInstance("#keepModal")
          Pop.success("You have successfully created a keep")
        } catch (error) {
          console.error('[CREATING KEEP]', error)
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
  width: 15rem;
  object-fit: cover;
}
</style>