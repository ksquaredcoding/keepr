import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("/api/keeps")
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async setActiveKeep(keepId) {
    AppState.activeKeep = null
    const keep = await this.getKeepById(keepId)
    AppState.activeKeep = keep
  }

  async createKeep(keepData) {
    const res = await api.post("/api/keeps", keepData)
    const keep = new Keep(res.data)
    AppState.keeps = [keep, ...AppState.keeps]
    AppState.profileKeeps = [keep, ...AppState.profileKeeps]
    AppState.activeKeep = null
    AppState.activeKeep = keep
  }

  async getProfileKeeps(profileId) {
    const res = await api.get(`/api/profiles/${profileId}/keeps`)
    AppState.profileKeeps = res.data.map(k => new Keep(k))
  }

  async getKeepById(keepId) {
    const res = await api.get(`/api/keeps/${keepId}`)
    const keep = new Keep(res.data)
    if (!keep) {
      Pop.error("Could not find keep")
    }
    return keep
  }

  async removeMyKeep(keepId) {
    const keep = await this.getKeepById(keepId)
    if (keep.creatorId != AppState.account.id) {
      Pop.error("You can only delete your own keeps")
    }
    await api.delete(`/api/keeps/${keep.id}`)
    AppState.profileKeeps = AppState.profileKeeps.filter(k => k.id != keep.id)
  }
}

export const keepsService = new KeepsService()