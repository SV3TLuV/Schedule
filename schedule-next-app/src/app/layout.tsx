import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Inter } from 'next/font/google'
import React from "react";
import {Metadata} from "next";
import {Providers} from "@/app/providers";
import {AppNav} from "@/components/Navbar/navbar";

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
    title: 'Расписание',
}

export default function RootLayout({ children }: { children: React.ReactNode }) {
    return (
        <html lang="en">
            <body className={inter.className}>
                <Providers>
                    <AppNav/>
                    {children}
                </Providers>
            </body>
        </html>
    )
}
