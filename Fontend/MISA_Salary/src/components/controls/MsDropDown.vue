<template>
  <div class="dropdown-panel" ref="dropdownRef">
    <!-- Tất cả trạng thái -->
    <div
      class="row"
      :class="{ disabled: SaveStatus === null }"
      @click="selectStatus(null)"
    >
      <div class="inRow">
        <div>Tất cả trạng thái</div>
        <div
          v-if="SaveStatus === null"
          class="icon-slot-hidden"
          style="margin-left: 8px"
        ></div>
      </div>
    </div>

    <!-- Đang theo dõi -->
    <div
      class="row"
      :class="{ disabled: SaveStatus === 1 }"
      @click="selectStatus(1)"
    >
      <div class="inRow">
        <div>Đang theo dõi</div>
        <div
          v-if="SaveStatus === 1"
          class="icon-slot-hidden"
          style="margin-left: 8px"
        ></div>
      </div>
    </div>

    <!-- Ngừng theo dõi -->
    <div
      class="row"
      :class="{ disabled: SaveStatus === 0 }"
      @click="selectStatus(0)"
    >
      <div class="inRow">
        <div>Ngừng theo dõi</div>
        <div
          v-if="SaveStatus === 0"
          class="icon-slot-hidden"
          style="margin-left: 8px"
        ></div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { onClickOutside } from "@vueuse/core";
const SaveStatus = ref();
// gửi trạng thái lên cha
const emit = defineEmits(["selected", "close"]);
const selectStatus = (status) => {
  emit("selected", status);
};
// nhận trạng thái từ cha
const prop = defineProps({ modelValue: Number });
// cha đổi trạng thái -> con đổi theo
watch(
  () => prop.modelValue,
  (a) => {
    SaveStatus.value = a;
  },
);
// sự kiện click ngoài màn hình
const dropdownRef = ref(null);
onClickOutside(dropdownRef, () => {
  emit("close");
});
</script>

<style scoped lang="scss">
.dropdown-panel {
  position: absolute;
  top: 110%;
  left: 0;
  width: 200px;
  background: #fff;
  border-radius: 8px;
  padding: 6px 0;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.16);
  z-index: 10;
  padding: 8px 6px;

  .row {
    justify-content: space-between;
    position: relative;
    z-index: 1000000;
    border-radius: 4px;
    width: 100%;
    height: 37px;
    margin: 0;
    font-weight: 400 !important;
    font-size: 14px;
    display: flex;
    align-items: center;
    border-radius: 4px;
    cursor: pointer;

    .inRow {
      justify-content: space-between;
      width: 100%;
      height: 100%;
      position: relative;
      display: flex;
      align-items: center;
      padding: 0 8px;
    }
  }

  .row:hover {
    color: #34b057;
    background-color: rgb(234, 251, 242);
  }
}

.disabled {
  color: #34b057;
  background-color: rgb(234, 251, 242);
}
</style>
