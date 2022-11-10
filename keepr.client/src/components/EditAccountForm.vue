<template>
  <div class="modal fade" id="editAccountForm" tabindex="-1" aria-labelledby="editAccountFormLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="col-md-6">
                <div>
                  <img :src="editable.picture" alt="profile picture preview"
                    class="mt-2 preview-img animate_animated animate_fadeIn">
                </div>
                <div class="form-group">
                  <label for="picture">Profile Picture:</label>
                  <input type="url" v-model="editable.picture" required class="form-control"
                    placeholder="Give your profile a cool cover image">
                </div>
                <div class="form-group">
                  <label for="name"> Name:</label>
                  <input type="text" v-model="editable.name" placeholder="Name of vault" required class="form-control">
                </div>
              </div>
              <div class="col-md-6">
                <div>
                  <img :src="editable.coverImg" alt="cover image preview"
                    class="mt-2 preview-cover animate_animated animate_fadeIn">
                </div>
                <div class="form-group">
                  <label for="coverImg">Cover Image:</label>
                  <input type="url" v-model="editable.coverImg" required class="form-control"
                    placeholder="Give your profile a cool cover image">
                </div>
              </div>
              <div class="my-3">
                <button class="btn btn-primary" type="submit" title="Save Account Changes">Save Changes
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
import { ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";


export default {
  setup() {
    const editable = ref({});
    watchEffect(() => {
      editable.value = { ...AppState.account };
    });
    return {
      editable,
      async handleSubmit() {
        try {
          await accountService.editAccount(editable.value);
          Modal.getInstance("#editAccountForm").hide()
          Pop.success("You have successfully edited your account")
        } catch (error) {
          console.error(error);
          Pop.error(error, "[EDITING ACCOUNT]");
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.preview-img {
  border-radius: 5%;
  border: dashed 4px #ababab;
  height: 10rem;
  width: 10rem;
  object-fit: cover;
}

.preview-cover {
  border-radius: 5%;
  border: dashed 4px #ababab;
  height: 15rem;
  width: 30rem;
  object-fit: cover;
}
</style>