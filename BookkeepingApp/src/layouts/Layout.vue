<template>
<div>
  <Header />
  <Sidebar />
</div>
  <div class="container">
    <router-view />
  </div>
</template>

<script>
import Header from "@/components/Header.vue";
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
body {
  font-family: sans-serif;
  font-size:13px;
}
p{
  font-size:12px;
}
div.content {
  margin: 45px 0px 0px 190px;
  padding: 10px 0px 0px 20px;
  height: 100%;
}
</style>
