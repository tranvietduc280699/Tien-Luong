<template>
  <div class="modal-overlay">
    <div class="coating"></div>
    <div class="modal-box">
      <!-- header -->
      <div class="modal-header">
        <div class="title" v-if="prop.data === `custom`">Chuyển trạng thái</div>
        <div class="title" v-else>Thông báo</div>
        <div class="modal-delete" @click="closeModal">
          <div class="icon-close"></div>
        </div>
      </div>

      <!-- hiển thị phần chuyển trạng thái -->
      <div
        class="modal-body"
        v-if="
          (prop.data === `custom` && currentStatus === 1) ||
          prop.data === `listOffStatus`
        "
      >
        Bạn có chắc chắn muốn chuyển trạng thái thành phần lương
        <span class="text" v-if="prop.data === `custom`">{{
          showTextNameforStatus
        }}</span>
        <span v-if="prop.data === `listOffStatus`">đã chọn</span>
        sang ngừng theo dõi không?
      </div>
      <!--  -->
      <div
        class="modal-body"
        v-if="
          (prop.data === `custom` && currentStatus === 0) ||
          prop.data === `listOnStatus`
        "
      >
        Bạn có chắc chắn muốn chuyển trạng thái thành phần lương
        <span class="text" v-if="prop.data === `custom`">{{
          showTextNameforStatus
        }}</span>
        <span v-if="prop.data === `listOnStatus`">đã chọn</span>
        sang đang theo dõi không?
      </div>

      <!-- hiển thị phần xóa bản ghi -->
      <div class="modal-body" v-if="prop.data === `single`">
        Bạn có chắc chắn muốn xóa thành phần lương
        <span class="text">{{ showTextName }}</span>
        không?
      </div>
      <div class="modal-body" v-if="prop.data === `multi`">
        Bạn có chắc chắn muốn xóa thành phần lương đã chọn không ?
      </div>

      <!-- danh mục thành phần lương -->
      <div class="modal-body" v-if="showLabelSystem">
        Bạn có chắc chắn muốn đưa thành phần lương mặc định
        <span class="text">{{ showLabelSystem }}</span> vào danh sách sử dụng
        không?
      </div>
      <!--  bottom -->
      <div class="modal-footer">
        <div class="btn-edit" @click="closeModal">
          <div class="cancel">Hủy</div>
        </div>
        <!--  xóa -->
        <div
          class="btn-delete"
          v-if="prop.data === `single` || prop.data === `multi`"
          @click="onDeleteRow"
        >
          <div>Xóa</div>
        </div>
        <!-- đồng ý chuyển trạng thái -->
        <div
          class="btn-status"
          v-if="
            prop.data === `custom` ||
            showLabelSystem ||
            prop.data === `listOffStatus` ||
            prop.data === `listOnStatus`
          "
          @click="eventClick"
        >
          <div>Đồng ý</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { computed } from "vue";
// hiển thị tên thành phần ( xóa bản ghi)
const showTextName = computed(() => prop.rowData?.salaryCompositionCode);
// hiển thị tên thành phàn (chuyển trạng thái)
const showTextNameforStatus = computed(
  () => prop.rowStatusData?.salaryCompositionCode,
);
const currentStatus = computed(() => prop.rowStatusData?.status);
// hiển thị tên thành phần danh mục hệ thống
const showLabelSystem = computed(
  () => prop.salarySystemData?.salaryCompositionSystemName,
);
// gửi lên cha
const emit = defineEmits(["closeModal", "onDeleteRow", "eventClick"]);
const closeModal = () => {
  emit("closeModal");
};
const onDeleteRow = () => {
  if (!prop.data) return;
  emit("onDeleteRow", prop.data);
};
const eventClick = () => {
  if (!prop.data) return;
  emit("eventClick", prop.data);
};
// theo dõi để cập nhật trạng thái cho header khi checkbox(list);
// nhận dl từ cha
const prop = defineProps({
  // single or multi or custom and listdataStatus
  data: {
    type: String,
    default: () => ({}),
  },
  // xóa bản ghi
  rowData: {
    type: Object,
    default: () => ({}),
  },
  // trạng thái
  rowStatusData: {
    type: Number,
    default: () => ({}),
  },
  // danh mục hệ thống
  salarySystemData: {
    type: Object,
    default: () => ({}),
  },
});
</script>
<style scoped lang="scss">
.modal-overlay {
  transition: all 0.2s;
  width: 100%;
  height: 100%;
  position: fixed;
  top: 0;
  z-index: 100;
  display: flex;
  align-items: flex-start;
  padding-top: 70px;
  justify-content: center;
  opacity: 1;

  .coating {
    width: 100%;
    background: rgba(0, 0, 0, 0.4);
    position: absolute;
    top: 0;
    height: 100%;
    z-index: 10;
    transition: all 0.25s ease;
    opacity: 1;
  }

  /* Box */
  .modal-box {
    background: rgb(255, 255, 255);
    width: 415px;
    min-width: 500px;
    position: relative;
    z-index: 100;
    margin: 10px;
    max-width: calc(100% - 30px);
    border-radius: 6px;
    box-shadow: 0 5px 20px 0 rgba(0, 0, 0, 0.1);
    background: #fff;
    overflow: visible;

    .modal-body {
      padding: 24px;
      font-size: 14px;
      font-weight: 400px;
      color: #333;

      .text {
        color: #000;
        font-weight: 600;
        font-size: 14px;
      }
    }

    .modal-body-list {
      padding: 24px;
      font-size: 16px;
      color: #333;
    }

    /* Header */
    .modal-header {
      width: 100%;
      display: flex;
      align-items: center;
      justify-content: space-between;
      border-radius: 4px 4px;
      position: relative;
      padding: 24px;
      padding-bottom: 0;
      height: 53px !important;
      font-weight: 700 !important;

      .title {
        letter-spacing: 0.576px;
        font-size: 24px;
        font-weight: 700;
        color: rgb(33, 33, 33);
      }

      .modal-delete {
        height: 36px;
        width: 36px;
        display: flex;
        border-radius: 50%;
        justify-content: center;
        align-items: center;
      }

      .modal-delete:hover {
        background-color: #f0f4f2;
      }
    }

    .modal-footer {
      padding: 16px 24px;
      height: 68px;
      display: flex;
      justify-content: flex-end;
      background: #f2f2f2;
      padding: 16px 24px;
      border-radius: 0 0 4px 4px;

      .btn-edit {
        width: auto !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;
        overflow: hidden;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
        display: flex;
        justify-content: center;
        align-items: center;
        margin-right: 8px;
      }

      .btn-edit:hover {
        border-color: #2ca64a;
        color: #2ca64a;
        cursor: pointer;
      }
      // xóa
      .btn-delete {
        width: auto !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;
        background-color: #e61d1d;
        color: #fff;
        border: 1px solid #e61d1d;
        font-size: 14px;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
      }
      // trạng thái
      .btn-status {
        width: auto !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;
        background-color: #34b057;
        color: #fff;
        border: 1px solid #34b057;
        font-size: 14px;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
      }
    }
  }
}
</style>
