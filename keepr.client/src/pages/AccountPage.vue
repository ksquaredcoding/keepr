<template>
  <div class="container-fluid">
    <div class="d-flex justify-content-center coverArea">
      <img class="profCover" :src="account?.coverImg" alt="profile cover image" v-if="account?.coverImg">
      <img class="profCover"
        src="https://images.unsplash.com/photo-1549321495-305eb13f8aa9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1169&q=80"
        alt="profile cover image" v-else>
      <img class="profileImg" :src="account?.picture" :alt="account?.name" :title="account?.name">
    </div>
    <div class="d-flex justify-content-around">
      <div></div>
      <div class="dropdown dropend">
        <button class="btn text-truncate dropdown-toggle fw-bold fs-5" type="button" data-bs-toggle="dropdown"
          aria-expanded="false" title="Account Options">
          ...
        </button>
        <ul class="dropdown-menu dropdown-menu-lg-right">
          <li><a class=" dropdown-item" data-bs-toggle="modal" data-bs-target="#editAccountForm"
              title="Edit Account">Edit
              Account</a>
          </li>
        </ul>
      </div>
    </div>
    <div class="row jusitfy-content-center text-center mt-5 pt-4">
      <h2>{{ account?.name }}</h2>
      <p>{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
    </div>
    <div class="row justify-content-center">
      <div class="col-10">
        <h1>Vaults</h1>
        <div class="row justify-content-center">
          <VaultCard v-for="v in vaults" :vault="v" :key="v.id" />
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-10">
        <h1>Keeps</h1>
        <div class="row ms-1 me-2 mt-1 g-4 no-y-over" v-masonry transition-duration="0.3s" item-selector=".item">
          <KeepCard v-for="k in keeps" :keep="k" :key="k.id" v-masonry-tile class="item" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import KeepCard from "../components/KeepCard.vue"
import VaultCard from "../components/VaultCard.vue"
import { accountService } from "../services/AccountService.js"
import { keepsService } from "../services/KeepsService.js"
import Pop from "../utils/Pop.js"
export default {
  setup() {
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
    async function getMyKeeps() {
      try {
        const keeps = await keepsService.getProfileKeeps(AppState.account?.id);
        return keeps;
      }
      catch (error) {
        console.error("[GETTING ACCOUNT KEEPS]", error);
        Pop.error(error.message);
      }
    }
    watchEffect(() => {
      if (AppState.account.id) {
        getMyVaults();
        getMyKeeps();
      }
    });
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.profileKeeps)
    };
  },
  components: { KeepCard, VaultCard }
}
</script>

<style scoped>
.profCover {
  width: 70vw;
  max-height: 30vh;
  border-radius: 8px;
  margin-top: 1rem;
}

.profileImg {
  border-radius: 50%;
  height: 60px;
  width: 60px;
  position: absolute;
  bottom: 0;
  margin-bottom: -30px;
}

.coverArea {
  position: relative;
}
</style>
