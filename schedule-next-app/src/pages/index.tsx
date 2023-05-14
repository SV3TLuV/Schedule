import {useGetDaysQuery} from "@/services/dayApi";

export default function Home() {
    const {data} = useGetDaysQuery()

    if (!data) {
        return (
            <div>Loading...</div>
        )
    }

    return (
        <>
            <ul>
                {data.items.map(day => (
                    <li key={day.id}>
                        {day.name}
                    </li>
                ))}
            </ul>
        </>
    )
}