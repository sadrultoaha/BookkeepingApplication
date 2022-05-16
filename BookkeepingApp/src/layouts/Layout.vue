<template>
  <div class="container content">
    <div class="row">
      <Header />
      <div class="col-md-2">
        <Sidebar />
      </div>
      <div class="col-md-8">
        <router-view />
      </div>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
import Footer from "@/components/Footer.vue";
import Sidebar from "@/components/Sidebar.vue";

export default {
  name: "Layout",
  components: {
    Header,
    Sidebar,
  },
};
</script>
<style>
.content {
  width: 100%;
  padding-left: 10%;
  padding-top: 5%;
  overflow: hidden;
}
</style>
