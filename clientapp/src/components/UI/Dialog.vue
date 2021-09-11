<template>
    <div class="dialog" v-if="show" @click="hide">
        <div @click.stop class="dialog__content">
            <slot></slot>
        </div>
    </div>
</template>

<script>
export default {
    name: 'g-dialog',
    props: {
        show: {
            type: Boolean,
            default: false
        }
    },
    methods: {
        hide() {
            this.$emit('update:show', false)
        }
    }
}
</script>

<style scoped>
.dialog {
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.5);
    position: fixed;
    display: flex;
    z-index: 2;
}

.dialog__content {
    margin: auto;
    
    background: white;
    border: 1px solid grey;
    border-radius: 2em;
    padding: 20px;
}
</style>