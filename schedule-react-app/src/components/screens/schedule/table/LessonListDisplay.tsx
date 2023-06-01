import {ITimetable} from "../../../../features/models/ITimetable.ts";
import {useEffect, useMemo, useRef, useState} from "react";
import {ILesson} from "../../../../features/models/ILesson.ts";
import {chunk} from "../../../../utils/chunk.ts";
import {CSSTransition} from "react-transition-group";
import {LessonDisplay} from "./LessonDisplay.tsx";

export const LessonListDisplay = ({timetable}: { timetable: ITimetable }) => {
    const [index, setIndex] = useState(0)
    const [show, setShow] = useState(false)
    const nodeRef = useRef(null);

    const lessons: ILesson[][] = useMemo(() => {
        if (timetable.lessons.length === 0)
            return []

        const chunkSize = 4
        return chunk(timetable.lessons, chunkSize)

    }, [timetable])

    useEffect(() => {
        const interval = setInterval(() => {
            const hasMore = lessons.length > 1

            if (hasMore) {
                setShow(false)
            }

            setTimeout(() => {
                setIndex((prev) => {
                    if (prev < lessons.length - 1) {
                        return prev + 1
                    }

                    return 0
                });

                if (hasMore) {
                    setShow(true)
                }
            }, 750)
        }, 7500);

        return () => {
            clearInterval(interval);
        };
    }, [lessons.length])

    return (
        <CSSTransition
            in={show}
            nodeRef={nodeRef}
            classNames='fade'
            timeout={1000}
        >
            <div
                className='h-100 w-100'
                ref={nodeRef}
            >
                {lessons?.[index]?.map((lesson, index) => (
                    <LessonDisplay
                        key={lesson?.id ?? index}
                        lesson={lesson}
                    />
                ))}
            </div>
        </CSSTransition>
    )
}