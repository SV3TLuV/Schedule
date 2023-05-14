import {AppProps} from "next/app";
import {Provider} from "react-redux";
import {wrapper} from "@/redux/store";

export default function App({ Component, ...refs }: AppProps) {
    const {store, props} = wrapper.useWrappedStore(refs)
    const {pageProps} = props

    return (
        <Provider store={store}>
            <Component {...pageProps}/>
        </Provider>
    )
}