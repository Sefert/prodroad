<script setup lang="ts">
</script>

<template>
    <nav class="navbar topnav navbar navbar-default navbar-fixed-top navbar-light bg-light">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Fixed top</a>
        </div>
    </nav>
</template>

<style scoped>
    .topnav {
        /*z-index: 1;  Stay on top */
        top: 0; /* Stay at the top */
        left: 0;
        background-color: #111; /* Black */
        padding-top: 20px;
    }
    
</style>