<template>
  <div class="container-fluid">

  </div>
  <div class="row ms-1 me-2 mt-1 row-cols-md-3 row-cols-1 g-4"
    data-masonry="{'percentPosition': true, 'itemSelector': '.col' }">
    <KeepCard v-for="k in keeps" :keep="k" :key="k.id" />
  </div>
</template>

<script>
import { computed } from "@vue/reactivity"
import { onMounted } from "vue"
import { AppState } from "../AppState.js"
import KeepCard from "../components/KeepCard.vue"
import { keepsService } from "../services/KeepsService.js"
import Pop from "../utils/Pop.js"

export default {
  setup() {
    async function getAllKeeps() {
      try {
        const keeps = await keepsService.getAllKeeps();
        return keeps;
      }
      catch (error) {
        console.error("[GETTING ALL KEEPS]", error);
        Pop.error(error.message);
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.homebody {
  position: relative;
}
</style>
