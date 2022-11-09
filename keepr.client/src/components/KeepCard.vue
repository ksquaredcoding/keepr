<template>
  <div class="col-md-3 keep-card selectable" data-bs-toggle="modal" data-bs-target="#keepModal"
    @click="setActiveKeep(keep?.id)">
    <div class="card keep-text">
      <img class="card-img" :src="keep?.img" :alt="keep?.name">
      <div class="card-img-overlay d-flex flex-column justify-content-end card-body">
        <div class="d-flex flex-row justify-content-between neg-marg">
          <div>
            <p class="fs-5 neg-marg"><strong>{{ keep?.name }}</strong></p>
          </div>
          <img class="creator-pic" :src="keep?.creator?.picture" :alt="keep?.creator?.name" :title="keep?.creator?.name"
            v-if="route.name == 'Home'">
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js"

export default {
  props: {
    keep: { type: Keep, required: true }
  },
  setup(props) {
    const route = useRoute();
    return {
      route,
      coverImg: computed(() => `url(${props.keep?.img})`),
      async setActiveKeep(keepId) {
        await keepsService.setActiveKeep(keepId)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-card {
  max-height: fit-content;
}

.keep-text {
  color: #eaeaea;
  text-shadow: 0px 0px 4px rgba(44, 44, 44, 0.616);
  letter-spacing: 0.05em;
}

.btm-row {
  position: absolute;
  flex-direction: row;
  justify-content: space-between;
  display: flex;
}

.creator-pic {
  border-radius: 50%;
  height: 2.5rem;
  width: 2.5rem;
}

.neg-marg {
  margin-bottom: -0.4rem;
}
</style>