<template>
  <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-body">
          <div class="row">
            <div class="col-md-6">
              <img class="img-fluid" :src="keep?.img" :alt="keep?.name">
            </div>
            <div class="col-md-6">
              <div class="d-flex flex-column justify-content-between">
                <div>
                  <p><i class="mdi mdi-eye-outline"></i> {{ keep?.views }}</p>
                  <p><i class="mdi mdi-alpha-k-box-outline"></i> {{ keep?.views }}</p>
                </div>
                <div>
                  <h1 class="text-center">{{ keep?.name }}</h1>
                  <p>{{ keep?.description }}</p>
                </div>
                <div class="d-flex flex-row justify-content-between">
                  <div>
                    <h3>collection</h3>
                    <button class="btn btn-dark">save</button>
                  </div>
                  <div>
                    <img class="creator-pic" :src="keep?.creator?.picture" :alt="keep?.creator?.name"
                      :title="keep?.creator?.name">
                    <p><strong>{{ keep?.creator?.name }}</strong></p>
                  </div>
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

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
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